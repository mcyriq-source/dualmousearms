using BepInEx;
using UnityEngine;

// ===============================================================
//  DualMouseArms.cs
//  Blueprint for a Gorilla Tag mod that uses TWO MICE as TWO ARMS
//  Written for beginners, readable, and fully explained.
//  This file shows the entire system in one place.
// ===============================================================

namespace DualMouseArms
{
    [BepInPlugin("cyriq.dualmouse.arms", "Dual Mouse Arms", "1.0.0")]
    public class DualMouseArms : BaseUnityPlugin
    {
        // -----------------------------
        // HAND TRANSFORMS (from GTAG)
        // -----------------------------
        private Transform playerBody;
        private Transform leftHand;
        private Transform rightHand;

        // -----------------------------
        // HAND POSITIONS + VELOCITIES
        // -----------------------------
        private Vector3 leftHandPos;
        private Vector3 rightHandPos;

        private Vector3 lastLeftHandPos;
        private Vector3 lastRightHandPos;

        private Vector3 playerVelocity;

        // -----------------------------
        // HAND ROTATIONS
        // -----------------------------
        private Quaternion leftRot = Quaternion.identity;
        private Quaternion rightRot = Quaternion.identity;

        // -----------------------------
        // SETTINGS (BALANCED LONG ARMS)
        // -----------------------------
        private float armLength = 1.4f;      // Slightly longer than normal
        private float pushStrength = 4.0f;   // How strong swings push you
        private float sensitivity = 0.15f;   // Mouse → rotation sensitivity

        // -----------------------------
        // RAW INPUT PLACEHOLDERS
        // (A real modder fills these in)
        // -----------------------------
        private Vector2 leftMouseDelta = Vector2.zero;
        private Vector2 rightMouseDelta = Vector2.zero;

        void Start()
        {
            // ----------------------------------------------------------
            // IMPORTANT:
            // A real modder must hook these to Gorilla Tag's transforms.
            // ----------------------------------------------------------
            playerBody = /* Player.Instance.transform */;
            leftHand   = /* Player.Instance.leftHandTransform */;
            rightHand  = /* Player.Instance.rightHandTransform */;

            // Initialize positions
            leftHandPos = leftHand.position;
            rightHandPos = rightHand.position;

            lastLeftHandPos = leftHandPos;
            lastRightHandPos = rightHandPos;

            // ----------------------------------------------------------
            // RAW INPUT SETUP GOES HERE
            // A real modder will implement:
            // - Device detection
            // - Raw mouse delta events
            // - Assign mouse #1 → left hand
            // - Assign mouse #2 → right hand
            // ----------------------------------------------------------
        }

        void Update()
        {
            float dt = Time.deltaTime;
            if (dt <= 0) return;

            // ----------------------------------------------------------
            // 1. UPDATE HAND ROTATIONS FROM MOUSE MOVEMENT
            // ----------------------------------------------------------
            leftRot  = UpdateHandRotation(leftRot, leftMouseDelta);
            rightRot = UpdateHandRotation(rightRot, rightMouseDelta);

            // ----------------------------------------------------------
            // 2. COMPUTE HAND POSITIONS FROM ROTATION + ARM LENGTH
            // ----------------------------------------------------------
            Vector3 shoulderLeft  = playerBody.position + playerBody.TransformDirection(new Vector3(-0.25f, 0.1f, 0.0f));
            Vector3 shoulderRight = playerBody.position + playerBody.TransformDirection(new Vector3( 0.25f, 0.1f, 0.0f));

            leftHandPos  = shoulderLeft  + leftRot  * Vector3.forward * armLength;
            rightHandPos = shoulderRight + rightRot * Vector3.forward * armLength;

            // ----------------------------------------------------------
            // 3. CALCULATE HAND VELOCITIES
            // ----------------------------------------------------------
            Vector3 leftVel  = (leftHandPos  - lastLeftHandPos)  / dt;
            Vector3 rightVel = (rightHandPos - lastRightHandPos) / dt;

            // ----------------------------------------------------------
            // 4. APPLY PUSH FORCES (REAL GTAG PHYSICS STYLE)
            // ----------------------------------------------------------
            ApplyPush(leftVel);
            ApplyPush(rightVel);

            // ----------------------------------------------------------
            // 5. MOVE PLAYER
            // ----------------------------------------------------------
            playerBody.position += playerVelocity * dt;

            // Add friction so you don’t slide forever
            playerVelocity *= 0.90f;

            // ----------------------------------------------------------
            // 6. UPDATE HAND TRANSFORMS IN GAME
            // ----------------------------------------------------------
            leftHand.position  = leftHandPos;
            rightHand.position = rightHandPos;

            leftHand.rotation  = leftRot;
            rightHand.rotation = rightRot;

            // ----------------------------------------------------------
            // 7. SAVE LAST POSITIONS
            // ----------------------------------------------------------
            lastLeftHandPos  = leftHandPos;
            lastRightHandPos = rightHandPos;

            // ----------------------------------------------------------
            // 8. RESET MOUSE DELTAS (RAW INPUT WILL FILL NEXT FRAME)
            // ----------------------------------------------------------
            leftMouseDelta = Vector2.zero;
            rightMouseDelta = Vector2.zero;
        }

        // ==============================================================
        // HAND ROTATION FROM MOUSE MOVEMENT
        // ==============================================================
        private Quaternion UpdateHandRotation(Quaternion current, Vector2 delta)
        {
            float yaw   = delta.x * sensitivity;
            float pitch = -delta.y * sensitivity;

            Quaternion yawRot   = Quaternion.AngleAxis(yaw,   Vector3.up);
            Quaternion pitchRot = Quaternion.AngleAxis(pitch, Vector3.right);

            return current * yawRot * pitchRot;
        }

        // ==============================================================
        // APPLY PUSH FORCE (REAL GTAG MOVEMENT)
        // ==============================================================
        private void ApplyPush(Vector3 handVel)
        {
            // Convert velocity to player's local space
            Vector3 localVel = playerBody.InverseTransformDirection(handVel);

            // If hand is moving TOWARD the body → push player
            if (localVel.z > 0f)
            {
                Vector3 pushDir = -handVel.normalized;
                float strength = handVel.magnitude * pushStrength;

                playerVelocity += pushDir * strength * Time.deltaTime;
            }
        }

        // ==============================================================
        // RAW INPUT STUBS (REAL MODDER FILLS THESE IN)
        // ==============================================================
        public void OnLeftMouseMove(int dx, int dy)
        {
            leftMouseDelta += new Vector2(dx, dy);
        }

        public void OnRightMouseMove(int dx, int dy)
        {
            rightMouseDelta += new Vector2(dx, dy);
        }
    }
}
