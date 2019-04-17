### 介绍
- 该存储库包含`Wopi Host`演示，及官方提供的示例项目`SampleWopiHandler`，
- 支持`DOCX`编辑，以及`PPTX`，`XLSX`
- 支持协同操作（Word、Excel、 PowerPoint）

### 注意
- 中文名称需要注意编码，(未实现)
- 运行`Wopi Host`需要 **管理员** 
- `Wopi Host`可以运行在`Office Online 2016`服务器，也可以运行在`Web`服务器

### 更新
#### [2019-04-17]
- MVC版本，可部署linux服务器（基于 Jexus）
- 添加一份 接口 文档

#### [2019-04-04]
- 更新Office Online Server补丁解决一些问题（实现图片上传、查看，word协同、PPT可用）

#### [2019-04-01]
-  更新官方提供的示例版本（ **IIS部署** ），`SampleWopiHandler` ， **推荐使用** 
- `WopiHost` 项目开始运行正常，隔段时间出现假死的状态，具体原因不知
- 实测，官方提供的示例版本  **响应速度快**、 **稳定** 
- <https://github.com/Microsoft/Office-Online-Test-Tools-and-Documentation>

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
##### 简单说明：
https://www.cnblogs.com/wanggege/p/4605678.html

添加角色和功能，选择 **Active Directory 域服务** 安装，等待完成，不要关闭，
点击 **将此服务器提升为域控制器** ，选择 **添加新林** ，输入根域名，如  **`oos.com`**  ，自己随便定义，输入密码，安装，自动重启

### 第四步：安装Office Online Server
没有包可以在，MSDN我告诉你 下载
```
ed2k://|file|cn_office_online_server_last_updated_march_2017_x64_dvd_10245068.iso|730759168|DA70F58CB8FFAF37C02302F2501CE635|/
```

##### Office Online Server注意事项：
https://docs.microsoft.com/zh-cn/officeonlineserver/plan-office-online-server

- 不要在装SQL Server等服务上运行
- 不要在端口 80、443 或 809 上安装依赖 Web 服务器 (IIS) 角色的任何服务或角色
- 不要安装任何版本的 Office
- 不要在域控制器上安装 Office Online Server
- 简而言之：一台干净的服务器

### 第五步：安装语言包
http://go.microsoft.com/fwlink/p/?LinkId=798136

### 打个补丁再继续
<https://download.microsoft.com/download/6/B/D/6BD1D664-1212-4AB2-9BE8-447731F2CA0E/wacserver2016-kb4011025-fullfile-x64-glb.exe>

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
编辑word
http://192.168.1.78/we/WordEditorFrame.aspx?WOPISrc=http://192.168.1.188:78/wopi/files/word.docx&ui=zh-CN&access_token=123456
查看word
http://192.168.1.78/wv/wordviewerframe.aspx?WOPISrc=http://192.168.1.188:78/wopi/files/word.docx&ui=zh-CN&access_token=123456
查看excel
http://192.168.1.78/x/_layouts/xlviewerinternal.aspx?WOPISrc=http://192.168.1.188:78/wopi/files/excel.xlsx&ui=zh-CN&access_token=123456
编辑excel
http://192.168.1.78/x/_layouts/xlviewerinternal.aspx?edit=1&WOPISrc=http://192.168.1.188:78/wopi/files/excel.xlsx&ui=zh-CN&access_token=123456
查看ppt
http://192.168.1.78/p/PowerPointFrame.aspx?PowerPointView=ReadingView&WOPISrc=http://192.168.1.188:78/wopi/files/ppt.pptx&ui=zh-CN&access_token=123456
编辑ppt
http://192.168.1.78/p/PowerPointFrame.aspx?PowerPointView=EditView&WOPISrc=http://192.168.1.188:78/wopi/files/ppt.pptx&ui=zh-CN&access_token=123456
```
- office服务地址可以是IP，也可是创建Office Online Server场指定的 **InternalURL**  ：`http://oos.com`
- `192.168.1.78` 是 office服务的IP地址，`192.168.1.188` 是 wopi服务，你得保证office服务器能访问wopi服务的地址
- token参数，用于授权验证，需要wopi服务里面实现
- WOPISrc参数需要加上`UserId`、`UserName`,表示当前的用户，协同操作，需要url编码
    - 示例：`http://192.168.1.188:78/wopi/files/word.docx?UserId=1&UserName=1`
    - 编码：`http%3A%2F%2F192.168.1.188%3A78%2Fwopi%2Ffiles%2Fword.docx%3FUserId%3D1%26UserName%3D1`

### 命令
- `Get-OfficeWebAppsFarm` 返回当前服务器所属的 OfficeWebAppsFarm 对象的详细信息
- `New-OfficeWebAppsFarm` 在本地计算机上创建新 Office Online Server 场
- `Set-OfficeWebAppsFarm` 配置现有 Office Online Server 场的设置
- `Remove-OfficeWebAppsMachine` 从 Office Online Server 场中删除现有服务器（删除Farm）
- 更多命令：<https://docs.microsoft.com/zh-cn/officeonlineserver/windows-powershell-for-office-online-server/windows-powershell-for-office-online-server>

### 截图

Word截图

![word](https://netnr.gitee.io/gs/2018/11/13/593bab5043.png)

Excel截图

![excel](https://netnr.gitee.io/gs/2018/11/13/852ec9c947.png)