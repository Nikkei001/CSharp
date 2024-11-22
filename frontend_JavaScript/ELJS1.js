// \是转义字符

const escaped = "\"hello world\""
// => "hello world"

const anotherLine = "hello \n world"
// => hello 
// world

const tab = "hello \t world"
// => hello    world

console.log(escaped)
console.log(anotherLine)
console.log(tab)

const trueorfalse = null == undefined
console.log(trueorfalse)