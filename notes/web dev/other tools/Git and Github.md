目的:方便自己和其他人看到你的整个项目代码
# Git

## 安装

安装的时候全部选默认选项
安装完成之后打开命令行工具(Git Bash),输入
`Git --version`
当返回
`git version 2.47.0.windows.2`
安装成功

## 常用命令

### cd定位到文件夹

`cd 文件夹路径`
可以直接拖动文件夹到命令行工具
执行该命令后,会显示
`路径$`
例如
`/d/Users/Administrator/Desktop/testing
$`

也可以打开Visual Studio Code,拖动文件夹,然后CTRL+\`(Tab上面那个键)打开终端(terminal)

### clear
清除terminal中已有的命令
### GIT INIT
初始化你的git repo
返回如下内容
`Initialized empty Git repository in D:/Users/Administrator/Desktop/testing/.git/`

### git config --global user.name和git config --global  user.email

用这两个命令设置用户和用户邮箱
示例
`git config --global user.name "Nikkei001"`
`git config --global  user.email "santates001@gmail.com"`

去掉--global,则只在当前仓库使用用户名和邮箱
### GIT ADD .
语法
`git add 文件名`
添加一个具体的文件
`git add .`
添加该目录下所有的文件

### GIT COMMIT
如果在GIT ADD之前没有设置用户名和邮箱,则会报错,如下所示
<blockquote style="background-color: #fff8dc; padding: 15px; border-radius: 5px; border-left: 4px solid #ffd700;"> 
如果在GIT ADD之前没有设置用户名和邮箱,则会报错,如下所示<br><br>
Author identity unknown<br><br>
git config --global user.email "you@example.com"<br>
git config --global user.name "Your Name"<br>
to set your account's default identity.<br>
Omit --global to set the identity only in this repository.
</blockquote>


设置好用户名和邮箱后,运行git commit命令
`git commit -m "initial commit"`
将当前的状态(快照)命名为"initial commit"
返回结果
\[master (root-commit) 787bd8d\] initial commit
 1 file changed, 13 insertions(+)
 create mode 100644 index.html

### GIT LOG
输入git log,返回结果
commit 787bd8d26f3275c069d40fb2d1bda47e9df2e196 (HEAD -> master)
Author: Nikkei001 <santates001@gmail.com>
Date:   Tue Nov 12 21:53:01 2024 +0800


## Github

### creat a repo in github

git commit上传快照之后就可以打开github了
首先, 在github创建一个repository
<details> 
	<summary>步骤</summary>
	<pre>
		<div><img src="https://ooo.0x0.ooo/2024/11/13/OHmrvc.png" alt="OHmrvc.png" border="0" width = 1500></div>
		<div><img src="https://ooo.0x0.ooo/2024/11/13/OHmAdr.png" alt="OHmAdr.png" border="0" width = 1500></div>
		<div><img src="https://ooo.0x0.ooo/2024/11/13/OHmZaM.png" alt="OHmZaM.png" border="0"></div>
		<p>然后选择https,复制出现的命令到vscode,运行即可<br>
		git remote add origin https://github.com/Nikkei001/CSharp.git<br>
		git branch -M main<br>
		git push -u origin main
		</p>
	</pre>
</details>


### Git clone

直接下载项目
直接下载下来的项目不能运行git命令,例如git log
使用git clone命令
使用该命令下载下来的项目保留了对commit的引用
报错:
fatal: protocol 'https' is not supported //=>可能是复制的链接有误
fatal: destination path 'testing-git' already exists and is not an emp
ty directory. //=> 
<details> 
	<summary>示例</summary>
	<pre>
		<img src="https://ooo.0x0.ooo/2024/11/20/OHELpx.png" alt="OHELpx.png" border="0">
		<img src="https://ooo.0x0.ooo/2024/11/20/OHEEYj.png" alt="OHEEYj.png" border="0">
	</pre>
</details>

## 理解常用的命令及其原理


## 报错处理

### vscode中sync changes报错

报错内容:

fatal: unable to access 'https://github.com/Nikkei001/Python.git/': Failed to connect to github.com port 443 after 21111 ms: Could not connect to server

解决方法:

terminal中输入如下命令:
`git config --global http.proxy 127.0.0.1:2080`

最后的IP地址是我使用的VPN的端口
注意你使用的vpn变化之后,对应的端口可能会变化,命令也会随之变化
例如
`git config --global http.proxy 127.0.0.1:7890`

解决办法2:
检查git的代理设置
`git config --global http.proxy`
如果返回类似`127.0.0.1:7890`这样的结果
运行
`git config --global --unset http.proxy`


