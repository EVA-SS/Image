# 镜像存储服务

#### 本案例使用 NET6.0 Winform 开发

使用 ASP [Minimal-API](https://learn.microsoft.com/zh-cn/aspnet/core/fundamentals/minimal-apis?source=recommendations&view=aspnetcore-6.0) 打造的文件存储服务，它不仅仅只限于Winform

应用实现了 `服务安装` `服务卸载` `服务启动` `服务停止` `磁盘挂载` `设置`

镜像存储实现了 `上传` `下载` `删除` `查询空间`

使用Windows服务来实现开机自启，能让服务开机后便用管理员权限启动

##### 分布式的想法

案例已完成存储服务所需功能，建议在调用端采用随机访问

*小型项目不建议使用分布式*

### 截图

![主页面](screenshot/1.png?raw=true)
![设置页面](screenshot/2.png?raw=true)
![挂载页面](screenshot/3.png?raw=true)