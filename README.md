# Dual Mouse Arms (Concept Mod for Gorilla Tag)

**Dual Mouse Arms** is a concept project that explores a wild idea:
using **two separate computer mice** as **two independent VR‑style arms**
inside Gorilla Tag.

This project provides a **complete blueprint** for how such a system
could work, including physics logic, hand simulation, and input design.
It is *not* a finished mod, but it contains everything a developer
would need to build one.

---

## 🎮 What This Project Does

This concept turns two mice into two hands:

- Mouse #1 → Left Hand  
- Mouse #2 → Right Hand  

Each mouse controls:
- Hand rotation  
- Hand direction  
- Hand velocity  
- Swing momentum  

The system then uses that velocity to push the player’s body forward,
just like real Gorilla Tag VR physics.

---

## 🧠 How It Works (Simple Explanation)

1. The mod reads raw input from **two different mice**.  
2. Each mouse moves and rotates one hand in 3D space.  
3. The system calculates hand velocity and acceleration.  
4. When a hand swings backward, the player is pushed forward.  
5. Movement is fully physics‑based — no teleporting or fake motion.

This makes PC movement feel surprisingly close to VR movement.

---

## 🦍 Features

- **Two independent hands** controlled by two mice  
- **Full 3D hand rotation** (yaw + pitch)  
- **Balanced long arms** for easier movement  
- **Realistic Gorilla Tag push physics**  
- **Single‑file blueprint (`DualMouseArms.cs`)**  
- **Beginner‑friendly comments and explanations**  

---

## 📁 What’s Included

This repository contains:

- `DualMouseArms.cs`  
  A single, giant, fully‑commented blueprint file showing the entire
  system in one place.

This file includes:
- Hand rotation logic  
- Hand position simulation  
- Physics calculations  
- Player movement logic  
- Raw input stubs for two mice  
- Clear comments for beginners  
- Notes for modders to finish the project  

---

## ⚠️ Important Notes

This is **not** a finished mod.  
It is a **blueprint** designed for learning, experimenting, and
collaboration.

A real developer will need to:
- Implement Windows Raw Input for two mice  
- Connect the script to Gorilla Tag’s actual player/hand transforms  
- Test and tune physics values  
- Build and compile the final DLL  

---

## 🧩 Why This Exists

Gorilla Tag has one of the most unique movement systems in gaming.
This project explores what happens when you try to recreate that
movement **without VR**, using only two mice and physics.

It’s meant to inspire:
- Modders  
- Developers  
- Experimenters  
- Content creators  
- Anyone who loves Gorilla Tag physics  

---

## 📣 Want to Help?

If you're a modder or developer and want to help turn this blueprint
into a real working mod, feel free to fork the project, open issues,
or contribute improvements.

---

## 📝 License

This project is open for learning, experimentation, and collaboration.
Use it however you like — just credit the original idea.But send me new one when someone finishes it thanks guys!
