# 可视化
# DAX基础
## 大体框架

## 常用函数
## CALCULATE

语法
```DAX
CALCULATE(<expression>, [[<filter1>], <filter2>]…)
```
expression--要求一个**返回scalar的表达式**,可以引用measure或其他在筛选上下文中可以计算的表达式
filter--任意数量的筛选器,可以是布尔表达式,表表达式,以及**筛选器修改函数**(filter modification functions),多个筛选器之间可以用逻辑运算符连接,如`AND`,`OR`.
CALCULATETABLE DAX 函数执行与 CALCULATE 函数完全相同的功能，但它**修改了应用于返回表对象的表达式的筛选上下文**。 在本模块中，解释和示例使用的是 CALCULATE 函数，但请记住，这些情况也可能适用于 CALCULATETABLE 函数。
<blockquote style="background-color: #fff8dc; padding: 15px; border-radius: 5px; border-left: 4px solid #ffd700;"> 
筛选器修改函数可以添加,移除和修改筛选器,常见的有<br>
<b>CALCULATE</b><br>
ALL,ALLEXCEPT<br>
REMOVEFILTERS,KEEPFILTERS<br>
FILTER,CROSSFILTER<br>
SELECTEDVALUE,VALUES<br>USERELATIONSHIP
</blockquote>

### 布尔表达式筛选器(boolean filter expression)

布尔表达式是返回true或false的表达式,须遵守三条规则
1. 只能引用一列
2. 不能引用measures
3. 不能使用扫描或返回包含聚合函数（如 SUM）的表的函数

<details> 
	<summary>示例</summary>
	<pre>
		<code class="language-dax">
		// 筛选红色或蓝色的记录<br>
		Revenue Red or Blue = CALCULATE([Revenue], 'Product'[Color] IN {"Red", "Blue"})<br>
		// 筛选价格大于1000的记录<br>
		Revenue Expensive Products = CALCULATE([Revenue], 'Product'[List Price] > 1000)<br>
		</code>
	</pre>
</details>

### 表表达式筛选器(table expression filters)

表表达式过滤器可以是对模型表的引用,也可以是一个返回表对象的DAX函数
通常,会使用FILTER函数应用复杂条件,它属于迭代函数(iterator function),需要传入的参数:
1. 表,或者表表达式
2. 计算表的每一行的表达式
FILTER函数返回的表对象和传入的表对象具有相同的结构,是传入的表对象的子集,代表参数2的结果为TRUE的行
**所有被传入CALCULATE函数的filter expression都是table filter expressions**.  **boolean filter expressions是table filter expressions的简写.**

<details> 
	<summary>示例</summary>
	<pre>
		<code class="language-dax">
				Revenue Red =
					CALCULATE(
					    [Revenue],
					    FILTER(
					        'Product',
					        'Product'[Color] = "Red"
					    )
					)
				// 简写
				Revenue Red = CALCULATE([Revenue], 'Product'[Color] = "Red")
		</code>
	</pre>
</details>

## 非常重要的基本概念

### 行上下文--row context

只有计算列(calculated column)和迭代函数会自动创建行上下文


### 筛选上下文--filter context

#### 概述
1.filter context作用在measures的计算过程中
2.filter可以直接作用在列上
3.当不同的表存在关系,filter可以**通过关系间接传播到其他表**
<blockquote style="background-color: #fff8dc; padding: 15px; border-radius: 5px; border-left: 4px solid #ffd700;"> 
calculated tables和calculated columns不会在filter context中进行计算。 calculated columns是在row context中计算的，不过如果需要汇总模型数据，公式可以将row context转换为filter context.
</blockquote>

参考[[PowerBIBasics#DAX基础#非常重要的基本概念#筛选上下文--filter context#上下文转换]]

4.在设计report的场景中
-filter可以在可视化对象(visual)和filter面板中使用
-切片器可视化对象可以筛选report page(==以及配置为同步切片时的其他页面?==)
-执行分组的可视化对象存在一个隐藏的filter
在设计report之外的场景中
-在filter面板可以修改设置
-可以选择可视化对象的元素(例如columns,bars,pie chart segments)做==**cross-filter或者cross-highlight**==
5.在进行复杂计算时需要添加修改或删除filter得到正确结果
<details> 
<summary>示例</summary>
<pre>
<img src="https://ooo.0x0.ooo/2024/10/30/ODw7Yj.png" alt="ODw7Yj.png" border="0" width=500>
<p>
假设有一个示例，要求公式修改筛选器上下文。 你的目标是生成一个报表视觉对象，其中显示每个销售区域及其收入和收入占总收入的百分比。<br>
“收入占总区域收入的百分比”结果是通过定义一个度量值表达式来实现的，即收入除以所有区域收入的比率。因此，对于澳大利亚，比率是 10655335.96 美元除以 109809274.20 美元，即 9.7%。<br>
分子表达式不需要修改筛选器上下文；它应使用当前筛选器上下文(按区域分组、适用于该区域筛选器的视觉对象)<br>
分母表达式需要删除任何区域筛选器，以实现所有区域的结果。
</p>
</pre>
</details>


#### CALCULATE函数
参考:[[PowerBIBasics#DAX基础#CALCULATE]]

最强大的 DAX 函数之一，允许你在**计算公式时修改筛选器上下文**。


#### 上下文转换


## 添加度量值(measures)
### 度量值分类
implicit measures
explicit measures

### 添加隐式度量值(implicit measures)
查找原文:
https://learn.microsoft.com/en-us/training/modules/dax-power-bi-add-measures/1-introduction
1.Data中的fields带sigma符号代表什么?
2.可以设置**Summarization属性**控制fields是否可以聚合
<details> <summary>位置</summary>
<pre>
<img src="https://ooo.0x0.ooo/2024/10/26/OD66gY.png" alt="OD66gY.png" border="0" width=1600>
</pre>
</details>
按照如图所示的顺序操作,选中Dont Summarize,则被选中的field左侧的sigma符号会消失
3.调整聚合的方式
4.汇总数值列和非数值列 支持的聚合函数(aggregation functions)也不一样
5.隐式度量值的不足--可能会产生错误的汇总,不支持复杂需求,==不支持MDX语言==


### 添加显式度量值(explicit measures)
查找原文:
添加简单的measures
https://learn.microsoft.com/en-us/training/modules/dax-power-bi-add-measures/2-simple-measures
添加复合的measures
https://learn.microsoft.com/en-us/training/modules/dax-power-bi-add-measures/3-compound-measures
quick measures
https://learn.microsoft.com/en-us/training/modules/dax-power-bi-add-measures/4-quick-measures
对比calculated columns和measures
https://learn.microsoft.com/en-us/training/modules/dax-power-bi-add-measures/5-compare-calculated-columns-measures
1.你可以通过编写DAX函数添加一个measure,这个函数必须返回一个标量(scalar或者single value),标量可以是数字,文本,日期,布尔值等等,不能是列表,表格,列等等
<details> <summary>包含多个元素的示例</summary>
示例1 - Scalar/Single Value(正确的度量值)
<pre><code class="language-dax"> 
Total Sales = SUM(Sales[Amount]) // 返回一个数字，比如 1000 
Average Price = AVERAGE(Products[Price]) // 返回一个数字，比如 25.99 
Customer Count = DISTINCTCOUNT(Customers[CustomerID]) // 返回一个数字，比如 150 
</code></pre>
示例2 - 错误的度量值
<pre><code class="language-dax">
// 错误示例1：返回多行数据 
Bad Measure = Sales[Amount] // 错误，因为这会返回整个Amount列 
// 错误示例2：返回表格 
Bad Measure = FILTER(Sales, Sales[Amount] > 1000) // 错误，因为这会返回一个表格
</code></pre>
</details>
<blockquote style="background-color: #fff8dc; padding: 15px; border-radius: 5px; border-left: 4px solid #ffd700;"> 
calculated只用来描述表和列,例如calculated tables和calculated columns,和power query中的tables和columns作区分,不存在calculated measures这个概念 
</blockquote>

2.创建measures,调整小数点位数,建议在创建度量值后立刻调整Formatting选项
3.你可以在report view中隐藏一列,然后在table view中取消隐藏和隐藏列
4.介绍了COUNT,DISTINCTCOUNT,COUNTROWS,前两个接受一列,最后一个接受表,COUNT和COUNTROWS返回非空值(non-blank values)的个数(非空的),DISTINCTCOUNT返回一列中distinct values的个数
5.聚合了单列或单表的measures都是简单的measures.
最后给出一个示例
<details> <summary>示例</summary>
<pre><code class="language-dax"> 
Order Line Count =
COUNT(Sales[SalesOrderLineKey])
Order Line Count = COUNTROWS(Sales)
</code></pre>
</details>
6.当一个measures引用了一个或多个measures,称其为compound measure
<details> <summary>示例</summary>
<pre><code class="language-dax"> 
Profit =
[Revenue] - [Cost]
</code></pre>
</details>
7.添加一个quick measure,实际上就是按照提示填入参数
8.区分measures和calculated columns
<details> <summary>区别</summary>
<pre>
1.calculated columns给table增加了一个新的列,measures定义如何汇总模型的数据
2.calculated columns是在数据刷新时使用row context进行计算的，而measures是在查询时使用filter context进行计算的。 filter context将在后面的模块中介绍；这是一个需要理解和掌握的重要主题，以便实现复杂的汇总。
3.calculated columns在每一行存储值,measures不在模型中存储值
4.calculated columns在visual中可以用来filter,group,summarize,而measures只能summarize
</pre>
</details>

## 迭代器函数(iterator functions)
迭代器函数需要filter context的前置知识:
[[PowerBIBasics#DAX基础#筛选上下文-filter context]]
### 简介
1.每个汇总单列的函数都有一个对应的iterator functions, 例如SUM, 对应的iterator function就是SUMX,实际上,在PBI内部,SUM函数会被转换为SUMX函数
常见的有SUMX, COUNTX, MINX, MAXX等等
<details> 
	<summary>示例</summary>
	<pre>
		<code class="language-dax">
				Revenue = SUM(Sales[Sales Amount])
		</code>
	</pre>
	<pre>
		<code class="language-dax">
				Revenue =
				SUMX(
				    Sales,
				    Sales[Sales Amount]
				)
		</code>
	</pre>
	<p>
		了解iterator functions的上下文工作原理非常重要。 由于iterator functions对表格行进行枚举，因此会在row context中对每一行的表达式进行计算，这与calculated columns公式类似。 table是在filter context中进行评估的，因此，如果使用之前的 "收入 "度量定义示例，如果报表可视化是按 2020 财政年度筛选的，那么 "Sales"表将包含在该年度的行。
	</p>
</details>
2.iterator functions必须要传入一个table和一个表达式,table可以是一个table的引用,也可以是一个返回table对象的表达式
<blockquote style="background-color: #fff8dc; padding: 15px; border-radius: 5px; border-left: 4px solid #ffd700;"> 
使用iterator functions时，请确保避免在使用扩expansive DAX 函数的表达式中使用大型表格（行）。 有些函数，如 SEARCH函数（扫描文本值，查找特定字符或文本）可能会导致性能缓慢。 此外，LOOKUPVALUE函数可能会导致缓慢的逐行检索值,请尽可能使用 RELATED函数。
</blockquote>

### 使用aggregation iterator functions
#### complex summarization
##### 涉及到一张表的summarization


##### 涉及到多张表的summarization



#### high grain summarization
##### 什么是粒度(grain)
在DAX中，Grain（粒度）指的是数据的细化程度，或者说数据被分解的最小单位。你可以把它想象成数据的“颗粒度”。

细粒度 (Fine Grain): 数据被拆分得非常细致，每个数据点都包含了尽可能多的细节信息。例如，一个销售订单表，每一行代表一个订单，这就是一个细粒度的表。
粗粒度 (Coarse Grain): 数据被聚合到更高的层次，丢失了一些细节信息。例如，将销售订单表按月份聚合，得到每个月的销售额，这就是一个粗粒度的汇总。


#### 计算排名(RANKX)
语法
```DAX
RANKX(<table>, <expression>[, <value>[, <order>[, <ties>]]])
```
table--输入表
expression--输入表达式
这两个参数和其他的iterator functions一样
value--rank value
order--如果不输入参数默认降序(DESC,较大的值排名较低),想象一下,如果参与排序的列是升序,那么排名是降序,如果参与排序的列是降序,排名是升序(原文:**The highest result should be assigned the lowest rank.** )
ties(并列值)--输入该参数确定是否采用dense ranking,如果是,遇到并列值会使用下一个rank value(例如13,13,14),如果不是,则使用skip的策略(例如13,13,15),如果不输入该参数,则采用skip的策略,具体可以参考下面的示例

如果需要使总量不参与排名,可以使用HASONEVALUE函数


### 其他需注意的内容
#### 使用计算列和迭代函数的建议
如果计算逻辑简单，且计算结果不需要频繁更新，优先使用计算列。
如果计算逻辑复杂，或者需要根据不同的上下文产生不同的结果，使用SUMX一类函数。
<details> 
	<summary>示例</summary>
		<p>
			计算总销售额：<br>
			SUMX： 总销售额 = SUMX(销售, 销售[销售额])<br>
			计算列： 创建一个计算列“总销售额”，公式为SUM(销售[销售额])<br><br>
			
			计算每个产品类别的平均销售额：<br>
			SUMX： 平均销售额 = AVERAGEX(VALUES(产品[类别]), CALCULATE(SUM(销售[销售额])))<br>
			计算列： 不适合使用计算列，因为平均销售额会根据筛选器变化。
		</p>
</details>




















