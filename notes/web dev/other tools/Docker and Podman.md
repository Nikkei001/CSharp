# Docker入门

## 一、Docker 是什么？先打个比方

想象你是个烘焙新手，家里只有烤箱和面粉，但想做曲奇饼干。这时你需要买黄油、糖、模具，还得查配方和温度。如果下次想做蛋糕，又要重新准备一堆东西，家里会变得乱糟糟。

Docker 就像一个 “万能收纳盒”。它把做曲奇或蛋糕需要的所有材料（软件运行环境、依赖库、配置文件）和制作步骤（代码）都打包好。不管你在哪个厨房（哪台电脑、哪个服务器），只要拿出这个盒子，就能直接开始制作，不用担心环境不匹配。

## 二、为什么要学 Docker？解决你的实际痛点

假设你开发了一个网页应用，在自己电脑上运行得很流畅。但部署到公司服务器上，却因为服务器的操作系统版本、Python 或 Java 版本不同，或者缺少某个依赖库，导致程序崩溃。这种 “在我电脑上明明可以” 的问题，就是开发和运维的噩梦。

Docker 能把你的应用和它运行所需的一切，从操作系统到软件包，都打包成一个 “镜像”。这个镜像可以在任何安装了 Docker 的环境中运行，完美解决环境不一致的问题。就像把你的应用装在一个 “魔法盒子” 里，不管放到哪里，打开就能用。

## 三、Docker 的核心概念：三个关键角色

1. **镜像（Image）**：相当于烘焙食谱 + 所有材料的清单，是一个只读的模板。比如官方的 Python 镜像，里面包含了 Python 运行环境和基础库。你可以基于它创建自己的镜像，添加自己的代码和依赖。
2. **容器（Container）**：镜像的 “运行实例”，就像根据食谱做出的曲奇饼干。容器是可以启动、停止、删除的，多个容器可以共享同一个镜像。每个容器都是隔离的，不会影响其他容器的运行。
3. **仓库（Repository）**：存放镜像的地方，类似超市。Docker Hub 是官方的公共仓库，里面有各种免费镜像。你也可以搭建自己的私有仓库，存放公司内部使用的镜像。

## 四、安装 Docker：跟着步骤走

### （一）Windows 系统

1. 打开 Docker 官网（[https://www.docker.com/](https://www.docker.com/)），点击 “Get Docker”，下载适合 Windows 的安装包。
2. 双击安装包，按照提示安装。安装完成后，会自动启动 Docker Desktop。
3. 打开命令提示符（CMD）或 PowerShell，输入`docker --version`，如果显示版本信息，说明安装成功。

### （二）Mac 系统

1. 同样在 Docker 官网下载 Mac 版安装包，双击安装。
2. 安装完成后，打开 Launchpad，找到 Docker 并启动。
3. 打开终端（Terminal），输入`docker --version`检查是否安装成功。

### （三）Linux 系统（以 Ubuntu 为例）

1. 打开终端，更新软件包列表：`sudo apt update`
2. 安装 Docker 所需的依赖包：`sudo apt install apt-transport-https ca-certificates curl gnupg lsb-release`
3. 添加 Docker 官方 GPG 密钥：`curl -fsSL https://download.docker.com/linux/ubuntu/gpg | sudo gpg --dearmor -o /usr/share/keyrings/docker-archive-keyring.gpg`
4. 添加 Docker 软件源：`echo "deb [arch=amd64 signed-by=/usr/share/keyrings/docker-archive-keyring.gpg] https://download.docker.com/linux/ubuntu $(lsb_release -cs) stable" | sudo tee /etc/apt/sources.list.d/docker.list > /dev/null`
5. 更新软件包列表并安装 Docker：`sudo apt update && sudo apt install docker-ce docker-ce-cli containerd.io`
6. 验证安装：`sudo docker --version`

## 五、第一个 Docker 容器：运行 Hello World

打开命令行，输入下面的命令：
bash
```bash
docker run hello-world
```

解释一下：
- `docker`：是操作 Docker 的命令
- `run`：表示运行一个容器
- `hello-world`：是镜像名称，这个镜像是 Docker 官方提供的测试镜像，用来验证 Docker 是否正常工作
如果看到类似下面的输出，恭喜你，已经成功运行了第一个 Docker 容器：
plaintext

```plaintext
Hello from Docker!
This message shows that your installation appears to be working correctly...
```

## 六、常用 Docker 命令：掌握这些就够用

1. **查看本地镜像**：`docker images`
2. **拉取镜像**：从 Docker Hub 下载镜像，比如拉取 Python 镜像：`docker pull python`
3. **运行容器**：`docker run -it ubuntu /bin/bash`
    - `-it`：以交互模式运行容器，并分配一个伪终端
    - `ubuntu`：镜像名称
    - `/bin/bash`：在容器中执行的命令，这里是打开 bash shell
4. **列出运行中的容器**：`docker ps`
5. **停止容器**：`docker stop <容器ID或名称>`
6. **删除容器**：`docker rm <容器ID或名称>`
7. **删除镜像**：`docker rmi <镜像ID或名称>`

## 七、进阶：创建自己的镜像

假设你写了一个简单的 Python 脚本`app.py`，内容如下：
python

```python
print("Hello, Docker!")
```

你可以按照以下步骤创建自己的镜像：
1. 在存放`app.py`的目录下，创建一个名为`Dockerfile`的文件（注意大小写，没有文件扩展名），内容如下：

Dockerfile

```dockerfile
# 使用官方Python 3镜像作为基础
FROM python:3

# 将当前目录复制到容器内的/app目录
COPY. /app

# 设置工作目录为/app
WORKDIR /app

# 安装脚本所需的依赖（如果有）
# RUN pip install some_package

# 定义容器启动时执行的命令
CMD ["python", "app.py"]
```

  

2. 打开命令行，进入`app.py`和`Dockerfile`所在的目录，执行以下命令构建镜像：
bash
```bash
docker build -t my-python-app.
```
解释：
 
- `-t`：给镜像指定一个标签（名称和版本号），这里名称是`my-python-app`
- `.`：表示 Dockerfile 在当前目录

3. 构建完成后，运行镜像：
 bash
```bash
docker run my-python-app
```

  你会看到输出`Hello, Docker!`，这就是你自己的镜像在容器中运行的结果。

## 八、总结与下一步

通过这篇教程，你已经了解了 Docker 的基本概念，学会了安装 Docker、运行容器、创建镜像等操作。接下来，你可以尝试：

1. 运行更复杂的应用，比如 Web 服务器（Nginx、Apache）、数据库（MySQL、PostgreSQL）。
2. 学习 Docker Compose，它可以帮助你管理多个容器的复杂应用。
3. 探索 Docker 在持续集成 / 持续部署（CI/CD）中的应用。

Docker 的世界还有很多有趣的内容等你发现，多动手实践，遇到问题可以在技术论坛（如 Stack Overflow）搜索解决方案，祝你学习愉快！
 
以上内容从基础概念到实际操作，助你快速入门 Docker。若你在学习中遇到问题，或想了解 Docker 更深入的内容，随时和我说。

希望这份教程能帮你顺利开启 Docker 学习之旅。要是在操作过程中有任何疑问，或是想了解 Docker 的其他进阶内容，欢迎随时和我交流。