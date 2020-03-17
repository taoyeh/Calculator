# 前言
&emsp;&emsp;虽然享受着博客的方便管理（~~感觉博客有点丑~~）= v =，但是还是想自己弄个服务器来记录生活和学习，自主性高一点，主要能放一些自己的写的项目运行，把自己的东西汇汇总，以后给别人看方便一点，服务器上也别省钱了，能白嫖的白嫖，能py的py，实在不行再买吧。(｀・ω・´)

# 问题描述

设计一个功能完整、界面美观的计算器应用程序。要求：
 - (1)    用户可通过鼠标点击按钮输入运算数，在文本框中显示，实现加、减、乘、除等四则运算及倒数、取反功能。要求实现良好的布局效果，通过对按钮功能分类简化编程。
- （2）	除了鼠标输入外，可同时实现键盘输入和运算(即通过键盘输入数字及运算符时,达到与按钮点击一样的效果)。
- （3）	实现小数运算和连续运算；
- （4）	实现sqrt，sin等科学计算功能；
- （5）	实现表达式解析等附加功能

# 解决思路
### 按钮很多，通过不同的事件进行区分
&emsp;&emsp;比如四则运算事件为DoudleClicked,而sqrt,x^2,1/x,sinx为SingleClicked,将不同的按钮分到同一个事件当中，代码的复用性高，逻辑清楚。

### 使用MSScriptControl来解决表达解释式
     SScriptControl.ScriptControl sc = new MSScriptControl.ScriptControl();
     sc.Language = "JavaScript";
     textBox1.Text= sc.Eval(textBox2.Text.ToString()).ToString();
&emsp;&emsp;其中 textBox1.Text为结果，textBox2.Text是一个字符串，比如  1+3*5-4/2 它能帮直接进行所需要的乘处优先，再算加减，所以这边我的代码是有问题的，在处理四则运算的时候，如果处理SingleClicked事件时就会起冲突。如果各位有兴趣的话还可以自己继续去研究效果图如下：

&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;![= =](https://i.loli.net/2020/03/17/G4PQiRCXvpwZ6mD.png)

### 源码附上 [项目传送门](https://github.com/taoyeh/Calculator)
