
Fast access to .net public and private fields/properties
=====================================

Forked from Marc Gravell for some project specific adjustments, like access to private fields as well as public.

##Original project:

An introduction to the reasons behind fast-member can be found <a href="http://marcgravell.blogspot.com/2012/01/playing-with-your-member.html" target="_blank">on Marc Gravell's blog</a>; example usage is simply:

```csharp
var accessor = TypeAccessor.Create(type); 
string propName = // something known only at runtime 
while( /* some loop of data */ )
{ 
  accessor[obj, propName] = rowValue; 
}
```
or
```csharp
// obj could be static or DLR 
var wrapped = ObjectAccessor.Create(obj);
string propName = // something known only at runtime 
Console.WriteLine(wrapped[propName]);
```