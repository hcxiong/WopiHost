### 介绍

该存储库包含Wopi Host演示

使用Office online 2016

支持DOCX编辑，以及PPTX，XLSX

### 要求

需要Office online 2016服务器

### 已知的问题

不支持协同操作

中文名称需要注意编码

### 用法和例子

http://`{domain}`/we/WordEditorFrame.aspx?WOPISrc=http%3a%2f%2flocalhost%3a800%2fwopi%2ffiles%2f`{word}`.docx&access_token=`{token}`&ui=zh-CN

http://`{domain}`/p/PowerPointFrame.aspx?PowerPointView=ReadingView&WOPISrc=http%3a%2f%2flocalhost%3a800%2fwopi%2ffiles%2f`{ppt}`.pptx&access_token=`{token}`&ui=zh-CN

http://`{domain}`/x/_layouts/xlviewerinternal.aspx?WOPISrc=http%3a%2f%2flocalhost%3a800%2fwopi%2ffiles%2f`{excel}`.xlsx&access_token=`{token}`&ui=zh-CN

### 安装教程

[部署 Office Online Server](https://docs.microsoft.com/zh-cn/officeonlineserver/deploy-office-online-server)

[服务器 Active Directory 域创建](https://www.cnblogs.com/wanggege/p/4605678.html)