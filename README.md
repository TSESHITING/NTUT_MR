# 👁️ Through Their Eyes: An MR Experience for Learning Disabilities

[!["Language: English"](https://img.shields.io/badge/Language-English-blue.svg)](#-english-version)
[!["語言：繁體中文"](https://img.shields.io/badge/語言-繁體中文-brightgreen.svg)](#-繁體中文版本)

[![Unity](https://img.shields.io/badge/Unity-2022.3+-blue.svg?style=flat&logo=unity)](https://unity.com/)
[![Platform](https://img.shields.io/badge/Platform-Meta%20Quest%203-lightgrey.svg)](https://www.meta.com/quest/quest-3/)
[![Tech](https://img.shields.io/badge/Tech-MR%20Passthrough-brightgreen.svg)]()

---

## 🚀 專案體驗與網站 / Project Links

* 📱 **AR App 體驗 (WebAR App)**：[點此進入 AR 應用](https://intheothesideofthemoonntut.netlify.app)
* 🌐 **專案網站 (Official Website)**：[點此瀏覽官方網站](https://intheothersideofthe-moon.netlify.app/#about)
* 💻 **網頁端專案庫 (WebAR Repo)**：[點此查看 WebAR 原始碼](https://github.com/TSESHITING/your-ar-repo-name)

---

## 👁️ 繁體中文版本

> **「他們不是懶惰，也不是不專心。他們只是透過不同的鏡頭在看這個世界。」**

這是一款專為 **Meta Quest 3** 開發的混合實境（MR）共情體驗專案，基於 **Unity** 引擎打造。本應用透過 **MR Passthrough（混合實境穿透）** 與圖像/物件偵測技術，真實模擬學習障礙兒童（如閱讀障礙）眼中所看到的文字世界——旨在幫助教師、家長和社會大眾親身體驗他們的日常掙扎，從而消除誤解、減少校園霸凌。

### 💡 專案故事與靈感來源

許多患有學習障礙的孩子，常被老師或同學誤解為「懶惰」、「不認真」或「故意不專心」，這種標籤往往導致孩子課業挫折，甚至引發嚴重的校園霸凌。

但事實上，他們的大腦在處理視覺資訊時與我們不同。為了喚起社會的同理心，本專案將他們眼中的真實視覺具現化：
* 🏃 **跳動的文字：** 單字與字母會無規則地漂移、跳動。
* 🔄 **顛倒與鏡像：** 文字會上下顛倒或前後反轉。
* 🌫️ **模糊與融化：** 字體邊緣變得模糊、甚至像融化一樣難以辨識，讓閱讀變成一場精疲力竭的戰鬥。

透過 MR 沉浸式體驗，我們希望將社會的「指責」轉化為「理解與包容」。

### 🛠️ 核心功能與技術棧

* **Meta Quest 3 Passthrough 整合：** 將使用者的真實物理環境與虛擬互動元素天衣無縫地結合。
* **即時物件/圖像偵測：** 當 Quest 3 相機偵測到特定的現實世界物件時，會立即在物理空間中生成（Instantiate）相對應的 **Unity Prefab**。
* **動態視覺混淆腳本/著色器：** 撰寫自訂的 Unity 腳本與 Shader 應用於文字組件上，動態模擬文字跳動、反轉與模糊的實時視覺效果。

### 🚀 開始使用與安裝指南

#### 開發環境需求
* Unity 2022.3 LTS (或更高版本)
* Meta XR All-in-One SDK
* Meta Quest 3 頭戴式裝置

#### 設定步驟
1. 複製本專案庫：
   ```bash
   git clone [https://github.com/TSESHITING/your-repo-name.git](https://github.com/TSESHITING/your-repo-name.git)
# 👁️ Through Their Eyes: An MR Experience for Learning Disabilities

[![Unity](https://img.shields.io/badge/Unity-2022.3+-blue.svg?style=flat&logo=unity)](https://unity.com/)
[![Platform](https://img.shields.io/badge/Platform-Meta%20Quest%203-lightgrey.svg)](https://www.meta.com/quest/quest-3/)
[![Tech](https://img.shields.io/badge/Tech-MR%20Passthrough-brightgreen.svg)]()

---

## 🚀 Project Links

* 📱 **WebAR App Experience**: [Click here to enter AR App](https://intheothesideofthemoonntut.netlify.app)
* 🌐 **Official Website (About)**: [Click here to visit official website](https://intheothersideofthe-moon.netlify.app/#about)
* 💻 **WebAR Source Repository**: [Click here to view WebAR Repo](https://github.com/TSESHITING/your-ar-repo-name) <!-- 💡 Replace with your actual WebAR GitHub URL -->

---

> **"They are not lazy, nor are they unfocused. They just see the world through a different lens."**

This is an empathy-driven project built with **Unity** for **Meta Quest 3**. By leveraging **MR Passthrough** and real-time image/object detection, this application simulates how children with learning disabilities (such as dyslexia) perceive written text. It aims to help educators, parents, and the general public experience their daily struggles firsthand, ultimately eliminating social misunderstandings and school bullying.

---

## 💡 The Story & Inspiration

Many children with learning disabilities are frequently misunderstood by teachers and peers as being "lazy," "unfocused," or "unwilling to learn." This stigma often leads to deep academic frustration and vulnerability to school bullying. 

In reality, their brain processes visual information uniquely. To raise social awareness, this project brings their visual reality to life:
* 🏃 **Jumping Text:** Words and letters shift and drift position erratically without clear rules.
* 🔄 **Inverted & Mirrored Imagery:** Written text appears upside down or flipped backward.
* 🌫️ **Blurry & Melting Vision:** Text edges dissolve and lose clarity, turning a simple reading task into an exhausting battle.

By placing users directly in their shoes through immersive Mixed Reality, we hope to transform external judgment into genuine empathy and inclusion.

---

## 🛠️ Project Ecosystem & Architecture

### 1. 🌐 WebAR Application (React + Mind-AR)
A lightweight, no-install-required mobile experience designed for quick accessibility via any smartphone browser.
* **Core Features:** Interactive video triggers linked to 11 unique QR codes. Scanning a code instantly streams the corresponding empathy scenario.
* **Tech Stack:** React / Vue, Mind-AR (Image Tracking), AI-assisted automation development tools.

### 2. 🥽 Meta Quest 3 MR Experience (Unity)
A fully immersive, premium spatial experience utilizing the advanced capabilities of hardware-accelerated pass-through.
* **Meta Quest 3 Passthrough Integration:** Blends the user's actual real-world physical environment seamlessly with interactive virtual overlays.
* **Real-time Object/Image Detection:** Instantly instantiates dynamic **Unity Prefabs** into physical space upon detecting specific real-world visual targets.
* **Dynamic Visual Confusion Shaders:** Custom HLSL shaders and C# scripts applied to text components to dynamically simulate text drifting, flipping, and blurring in real time.

---

## 🚀 Getting Started & Installation (MR Project)

### Prerequisites
* Unity 2022.3 LTS (or higher)
* Meta XR All-in-One SDK
* Meta Quest 3 Headset + Link Cable / Air Link

### Setup Instructions
1. Clone this repository:
   ```bash
   git clone [https://github.com/TSESHITING/your-repo-name.git](https://github.com/TSESHITING/your-repo-name.git)
