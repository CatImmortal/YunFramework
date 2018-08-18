# YunFramework
轻量级Unity3D客户端游戏框架，采用了单一脚本驱动设计（即全局只有一个继承了MonoBehaviour的驱动脚本），
实现了诸多游戏开发中的常见模块：
轮询器、
单例库、
数据结点、
有限状态机、
消息广播、
对象池、
执行结点、
UI管理、
资源管理（提供了Resources目录下的资源管理方案与AssetBundle机制的资源管理方案的实现）、
配置管理（提供了Xml与Json两种配置文件加载方案的实现）、
热更新（基于ILRuntime）、
本地数据库（基于IBoxDB）。
