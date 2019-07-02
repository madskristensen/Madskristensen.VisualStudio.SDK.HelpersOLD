# Visual Studio SDK Helpers
A helper library for Visual Studio extension development. Wraps services in easy-to-use async implementations that adopt best practices for reliability and performance.

## The VS namespace
All features are captured under a namespace called `VS` to make them easy to find.

```C#
await VS.StatusBar.SetTextAsync("This is great");
```

## Helpers included

* Status Bar
* Base command class
* Settings and options pages
* Projects and items

## Helpers planned...

* Error List
* Solution
* Source control