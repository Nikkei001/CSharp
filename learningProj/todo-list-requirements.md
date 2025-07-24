# To-Do List 项目需求文档

## 项目概述

### 项目名称
简易 To-Do List 管理系统

### 项目目标
本项目主要用于学习前后端交互技术，通过实现一个基础的待办事项管理系统来掌握：
- 前端与后端的数据交互
- RESTful API 的设计与实现
- 数据的增删改查操作
- 前后端分离架构
- PostgreSQL 数据库的使用

### 设计原则
- 简洁优先：采用白底黑字的简洁设计风格
- 功能导向：重点关注功能实现而非视觉效果
- 学习友好：代码结构清晰，便于理解前后端交互流程

## 技术栈

### 后端技术
- **语言**: C# (.NET Core/ASP.NET Core)
- **数据库**: PostgreSQL
- **ORM**: Entity Framework Core with Npgsql
- **API风格**: RESTful API
- **数据格式**: JSON

### 前端技术
- **语言**: HTML + CSS + JavaScript (原生)
- **样式**: 最小化CSS，白底黑字
- **交互**: Fetch API 进行HTTP请求

### 数据库配置
- **数据库**: PostgreSQL 12+ 
- **连接库**: Npgsql.EntityFrameworkCore.PostgreSQL
- **连接字符串示例**: 
  ```
  "Host=localhost;Database=todolist;Username=postgres;Password=your_password"
  ```

## 功能需求

### 核心功能

#### 1. 查看待办事项列表
- **功能描述**: 显示所有待办事项
- **API接口**: `GET /api/todos`
- **前端展示**: 简单的列表形式，每项包含内容、状态、操作按钮

#### 2. 添加新的待办事项
- **功能描述**: 用户可以添加新的待办事项
- **API接口**: `POST /api/todos`
- **前端交互**: 输入框 + 添加按钮
- **数据字段**:
  - `title` (string): 待办事项标题
  - `description` (string, 可选): 详细描述
  - `isCompleted` (boolean): 完成状态，默认false
  - `createdAt` (timestamp): 创建时间
  - `updatedAt` (timestamp): 更新时间

#### 3. 标记完成/未完成
- **功能描述**: 切换待办事项的完成状态
- **API接口**: `PUT /api/todos/{id}/toggle`
- **前端交互**: 复选框或切换按钮

#### 4. 编辑待办事项
- **功能描述**: 修改待办事项的标题和描述
- **API接口**: `PUT /api/todos/{id}`
- **前端交互**: 点击编辑按钮，内联编辑或弹出编辑框

#### 5. 删除待办事项
- **功能描述**: 删除指定的待办事项
- **API接口**: `DELETE /api/todos/{id}`
- **前端交互**: 删除按钮 + 确认提示

### 扩展功能（可选）

#### 6. 筛选功能
- 显示全部
- 只显示未完成
- 只显示已完成

#### 7. 搜索功能
- 根据标题搜索待办事项

