### 介绍

该存储库包含`Wopi Host`演示

支持`DOCX`编辑，以及`PPTX`，`XLSX`

### 要求

需要`Office Online 2016`服务器

### 注意

不支持协同操作（使用测试，`Excel支持`协同操作，Word **不** 支持）

中文名称需要注意编码

运行`Wopi Host`需要 **管理员** 

`Wopi Host`可以运行在`Office Online 2016`服务器上，也可以运行在`Web`服务器

### 用法和例子

http://`{domain}`/we/WordEditorFrame.aspx?WOPISrc=http%3a%2f%2flocalhost%3a800%2fwopi%2ffiles%2f`{word}`.docx&access_token=`{token}`&ui=zh-CN

http://`{domain}`/p/PowerPointFrame.aspx?PowerPointView=ReadingView&WOPISrc=http%3a%2f%2flocalhost%3a800%2fwopi%2ffiles%2f`{ppt}`.pptx&access_token=`{token}`&ui=zh-CN

http://`{domain}`/x/_layouts/xlviewerinternal.aspx?WOPISrc=http%3a%2f%2flocalhost%3a800%2fwopi%2ffiles%2f`{excel}`.xlsx&access_token=`{token}`&ui=zh-CN

### 安装教程

[部署 Office Online Server](https://docs.microsoft.com/zh-cn/officeonlineserver/deploy-office-online-server)

[服务器 Active Directory 域创建](https://www.cnblogs.com/wanggege/p/4605678.html)