### 介绍
- 支持`DOCX`编辑，以及`PPTX`，`XLSX`
- 支持协同操作（Word、Excel、 PowerPoint）
- 基于微软提供的示例稍稍调整
- <https://github.com/Microsoft/Office-Online-Test-Tools-and-Documentation>

### 调整
- 提取参数到`web.config`，方便配置
- 支持linux部署，基于jexus
- 支持中文参数
- 添加错误日志输出

### 说明
- 中文参数需先编码再传参数，`WOPISrc`参数再整体编码，即`编码两次`
- 文档根目录需要赋予`读写权限`
- 保存有延时，约30秒左右

### 参数
- 参数`access_token`为授权验证，需自己实现（协同针对同一token？应该是）
- 参数`UserId`为账号
- 参数`UserName`为姓名
- 参数是中文都需二次编码

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
点击 **将此服务器提升为域控制器** ，选择 **添加新林** ，输入根域名，如  **`oos.com`**  ，自己随便定义，输入密码，安装，自动重启，域服务已安装成

加入域，修改客户机 DNS 为 域服务器的IP地址，再修改工作组为域的方式

> 由于云服务器是克隆副本，加入域会报错：`无法完成域加入，原因是试图加入的域的 SID 与本计算机的 SID 相同`，解决方法，打开：`windows/System32/Sysprep/Sysprep.exe`，勾选 **通用** 重启

##### Office Online Server注意事项：
https://docs.microsoft.com/zh-cn/officeonlineserver/plan-office-online-server

- 不要在装SQL Server等服务上运行
- 不要在端口 80、443 或 809 上安装依赖 Web 服务器 (IIS) 角色的任何服务或角色
- 不要安装任何版本的 Office
- 不要在域控制器上安装 Office Online Server
- 简而言之：一台干净的服务器

### 第四步：安装Office Online Server

### 第五步：安装语言包

##### 版本号：16.0.8471.8525
安装包：`ed2k://|file|cn_office_online_server_last_updated_march_2017_x64_dvd_10245068.iso|730759168|DA70F58CB8FFAF37C02302F2501CE635|/`  
补丁包：<https://download.microsoft.com/download/6/B/D/6BD1D664-1212-4AB2-9BE8-447731F2CA0E/wacserver2016-kb4011025-fullfile-x64-glb.exe>  
语言包：<http://go.microsoft.com/fwlink/p/?LinkId=798136>

##### 版本号：16.0.10338.20039
安装包：<https://download.shareappscrack.com/s3/EfnW>  
语言包：<https://www.microsoft.com/zh-cn/download/details.aspx?id=51963>

[所有版本列表](https://docs.microsoft.com/zh-cn/officeonlineserver/office-online-server-release-schedule)

> 经测试，推荐安装版本号：16.0.8471.8525

### 第六步：导入OfficeWebApps 模块
```
Import-Module -Name OfficeWebApps
```
> 如果报错，需要重启

### 第七步：创建Office Online Server场
```
New-OfficeWebAppsFarm -InternalURL "http://001.oos.com" -AllowHttp -EditingEnabled
```
> 请以域管理账号登录

### 第八步：验证Office Online Server场
http://oos.com/hosting/discovery	域的方式访问，需要访问的客户端加入域

http://localhost/hosting/discovery	本地访问

http://192.168.1.78/hosting/discovery	IP访问	局域网都能访问，去看IIS，已经自动部署好站点

### 第九步：下载Wopi项目
https://github.com/netnr/WopiHost

https://gitee.com/netnr/WopiHost

启动服务，更改配置

### 第十步：使用
- [OfficeOnlineServer使用文档](https://www.netnr.com/doc/code/4964095842855914510)
- WopiHost 项目 `doc/OfficeOnlineServer.doc`

### 命令
- `Get-OfficeWebAppsFarm` 返回当前服务器所属的 OfficeWebAppsFarm 对象的详细信息
- `New-OfficeWebAppsFarm` 在本地计算机上创建新 Office Online Server 场
- `Set-OfficeWebAppsFarm` 配置现有 Office Online Server 场的设置
- `Remove-OfficeWebAppsMachine` 从 Office Online Server 场中删除现有服务器（删除Farm）
- 更多命令：<https://docs.microsoft.com/zh-cn/officeonlineserver/windows-powershell-for-office-online-server/windows-powershell-for-office-online-server>

### 截图

Word截图

![word](https://static.netnr.com/2018/11/13/593bab5043.png)

Excel截图

![excel](https://static.netnr.com/2018/11/13/852ec9c947.png)