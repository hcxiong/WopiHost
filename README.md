### 介绍

该存储库包含`Wopi Host`演示

支持`DOCX`编辑，以及`PPTX`，`XLSX`

### 注意

不支持协同操作（使用测试，`Excel支持`协同操作，Word **不** 支持）

中文名称需要注意编码

运行`Wopi Host`需要 **管理员** 

`Wopi Host`可以运行在`Office Online 2016`服务器，也可以运行在`Web`服务器


----------

> 安装教程：https://docs.microsoft.com/zh-cn/officeonlineserver/deploy-office-online-server

> 安装过程中，保证 **Windows Update** 服务可用

> 通过远程访问服务器安装时，不能通过资源共享磁盘的方式直接运行，但可以通过共享盘符的方式运行，如`\\192.168.1.188\G$`

> Server2012 R2 以上，本次以Server2016为例

> 运行`PowerShell`，由于是服务器，默认Administrator账号为 **管理员** 

### 第一步：角色和服务

Windows Server 2012 R2:
```
Add-WindowsFeature Web-Server,Web-Mgmt-Tools,Web-Mgmt-Console,Web-WebServer,Web-Common-Http,Web-Default-Doc,Web-Static-Content,Web-Performance,Web-Stat-Compression,Web-Dyn-Compression,Web-Security,Web-Filtering,Web-Windows-Auth,Web-App-Dev,Web-Net-Ext45,Web-Asp-Net45,Web-ISAPI-Ext,Web-ISAPI-Filter,Web-Includes,InkandHandwritingServices,NET-Framework-Features,NET-Framework-Core,NET-HTTP-Activation,NET-Non-HTTP-Activ,NET-WCF-HTTP-Activation45,Windows-Identity-Foundation,Server-Media-Foundation
```
Windows Server 2016：
```
Add-WindowsFeature Web-Server,Web-Mgmt-Tools,Web-Mgmt-Console,Web-WebServer,Web-Common-Http,Web-Default-Doc,Web-Static-Content,Web-Performance,Web-Stat-Compression,Web-Dyn-Compression,Web-Security,Web-Filtering,Web-Windows-Auth,Web-App-Dev,Web-Net-Ext45,Web-Asp-Net45,Web-ISAPI-Ext,Web-ISAPI-Filter,Web-Includes,NET-Framework-Features,NET-Framework-45-Features,NET-Framework-Core,NET-Framework-45-Core,NET-HTTP-Activation,NET-Non-HTTP-Activ,NET-WCF-HTTP-Activation45,Windows-Identity-Foundation,Server-Media-Foundation
```
### 第二步：环境依赖

 **.NET Framework 4.5.2** 

https://go.microsoft.com/fwlink/p/?LinkId=510096

 **Visual C++ Redistributable Packages for Visual Studio 2013** 

https://www.microsoft.com/download/details.aspx?id=40784

 **Visual C++ Redistributable for Visual Studio 2015** 

https://go.microsoft.com/fwlink/p/?LinkId=620071

 **Microsoft.IdentityModel.Extention.dll** 

https://go.microsoft.com/fwlink/p/?LinkId=620072

### 第三步：加入域
> 按要求，AD域服务器是独立的，本实例架设同一台服务器上

简单说明：

添加角色和功能，选择 **Active Directory 域服务** 安装，等待完成，不要关闭，
点击 **将此服务器提升为域控制器** ，选择 **添加新林** ，输入根域名，如  **`oos.com`**  ，自己随便定义，输入密码，安装，自动重启

帮助文档：https://www.cnblogs.com/wanggege/p/4605678.html

### 第四步：安装Office Online Server
没有包可以在，MSDN我告诉你 下载
```
ed2k://|file|cn_office_online_server_last_updated_march_2017_x64_dvd_10245068.iso|730759168|DA70F58CB8FFAF37C02302F2501CE635|/
```

### 第五步：安装语言包
http://go.microsoft.com/fwlink/p/?LinkId=798136

### 第六步：导入OfficeWebApps 模块
```
Import-Module -Name OfficeWebApps
```

### 第七步：创建Office Online Server场
```
New-OfficeWebAppsFarm -InternalURL "http://oos.com" -AllowHttp -EditingEnabled
```

### 第八步：验证Office Online Server场
http://oos.com/hosting/discovery	域的方式访问，需要访问的客户端加入域

http://localhost/hosting/discovery	本地访问

http://192.168.1.78/hosting/discovery	IP访问	局域网都能访问，去看IIS，已经自动部署好站点

### 第九步：下载Wopi项目
https://github.com/netnr/WopiHost

https://gitee.com/netnr/WopiHost

启动服务，更改config.json里面的配置

### 第十步：使用
```
http://{domain}/we/WordEditorFrame.aspx?WOPISrc=http%3a%2f%2flocalhost%3a800%2fwopi%2ffiles%2f{word}.docx&access_token={token}&ui=zh-CN

http://{domain}/p/PowerPointFrame.aspx?PowerPointView=ReadingView&WOPISrc=http%3a%2f%2flocalhost%3a800%2fwopi%2ffiles%2f{ppt}.pptx&access_token={token}&ui=zh-CN

http://{domain}/x/_layouts/xlviewerinternal.aspx?WOPISrc=http%3a%2f%2flocalhost%3a800%2fwopi%2ffiles%2f{excel}.xlsx&access_token={token}&ui=zh-CN

http://192.168.1.78/we/WordEditorFrame.aspx?WOPISrc=http%3a%2f%2flocalhost%3a800%2fwopi%2ffiles%2fword.docx&access_token=123456&ui=zh-CN

http://192.168.1.78/p/PowerPointFrame.aspx?PowerPointView=ReadingView&WOPISrc=http%3a%2f%2flocalhost%3a800%2fwopi%2ffiles%2fppt.pptx&access_token=123456&ui=zh-CN

http://192.168.1.78/x/_layouts/xlviewerinternal.aspx?WOPISrc=http%3a%2f%2flocalhost%3a800%2fwopi%2ffiles%2fexcel.xlsx&access_token=123456&ui=zh-CN
```
`domain` 是域，也可以是IP

### 截图
wopi服务截图

![wopi](https://netnr.gitee.io/gs/2018/11/13/70e7521d44.png)

Word截图

![word](https://netnr.gitee.io/gs/2018/11/13/593bab5043.png)

Excel截图

![excel](https://netnr.gitee.io/gs/2018/11/13/852ec9c947.png)