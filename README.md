<p align="center">
  <a>
    <img src="https://avatars.githubusercontent.com/u/98204361?s=200&v=4" height="96">
    <h3 align="center">Flame:fire:</h3>
  </a>
</p>

<p align="center">
  A programming language built to make powerful tasks simpler.
</p>

<p align="center">
  <a href="https://github.com/theflamelang/FlameSharp"><strong>Documentation</strong></a> ·
  <a href="https://github.com/theflamelang/FlameSharp"><strong>Examples</strong></a> ·
  <a href="https://github.com/theflamelang/FlameSharp"><strong>Website</strong></a>
</p>
<br/>

## Flame

Flame is a **compiled**, **data-oriented**, and **strongly typed** programming language designed to handle powerful tasks quickly, without any need of complicated low-level concepts. Flame uses a variety of systems that are designed to simplify tasks including memory management, stack/heap usage, and more!

## Documentation

no documentation yet!

## Examples

Fibonacci
```zig
func fib (i32 x) -> i32
{
  if x == 0 || x == 1 ->
  {
    return x;
  }
  else ->
  {
    return fib (x - 1) + fib (x - 2);
  }
}
```

Factorial
```zig
func fac (i32 x) -> i32
{
    return x == 0 ? 0 : fac (x - 1) * x; 
}
```

## Website

...and we dont have a website
