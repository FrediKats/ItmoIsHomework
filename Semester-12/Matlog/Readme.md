# Lambda parser

Цель: реализовать парсер лямбда выражений с поддержкой альфа и бета редукций.

## Пример работы

В `Program.cs` лежит пару примером выполнения. Например, мы можем выполнить бета редукцию на выражении "(λx.x) ((λa.λb.a b) (λy.y) (λz.z))". Первым шагом выполняется построение синтаксического дерева и восстановление из него выражения, чтобы убедиться, что структура не нарушена:

```text
(λx.x) ((λa.λb.a b) (λy.y) (λz.z)) <- ApplicationSyntaxNode
        (λx.x) <- ParenthesizedSyntaxNode
                λx.x <- AbstractionLambdaSyntaxNode
                        x <- ArgumentLambdaSyntaxNode
                        x <- ParameterLambdaSyntaxNode
        ((λa.λb.a b) (λy.y) (λz.z)) <- ParenthesizedSyntaxNode
                (λa.λb.a b) (λy.y) (λz.z) <- ApplicationSyntaxNode
                        (λa.λb.a b) (λy.y) <- ApplicationSyntaxNode
                                (λa.λb.a b) <- ParenthesizedSyntaxNode
                                        λa.λb.a b <- ApplicationSyntaxNode
                                                λa.λb.a <- AbstractionLambdaSyntaxNode
                                                        a <- ArgumentLambdaSyntaxNode
                                                        λb.a <- AbstractionLambdaSyntaxNode
                                                                b <- ArgumentLambdaSyntaxNode
                                                                a <- ParameterLambdaSyntaxNode
                                                b <- ParameterLambdaSyntaxNode
                                (λy.y) <- ParenthesizedSyntaxNode
                                        λy.y <- AbstractionLambdaSyntaxNode
                                                y <- ArgumentLambdaSyntaxNode
                                                y <- ParameterLambdaSyntaxNode
                        (λz.z) <- ParenthesizedSyntaxNode
                                λz.z <- AbstractionLambdaSyntaxNode
                                        z <- ArgumentLambdaSyntaxNode
                                        z <- ParameterLambdaSyntaxNode

[19:48:25 INF] Diff:
[19:48:25 INF] (λx.x) ((λa.λb.a b) (λy.y) (λz.z))
[19:48:25 INF] (λx.x) ((λa.λb.a b) (λy.y) (λz.z))
```

После того, как дерево построено, по нему генерируется семантическое дерево. Основная задача семантического дерева - связать арументы лямбда выражений и их использования в теле выражения (далее использования будут называться параметрами). В примере производится поиск первой ноды аппликации и на ней вызывается редукция. В выражении из примера аргументом будет `((λa.λb.a b) (λy.y) (λz.z))`, а применяться будет на `(λx.x)`:

```text
[19:48:25 INF] Diff:
[19:48:25 INF] (λx.x) ((λa.λb.a b) (λy.y) (λz.z))
[19:48:25 INF] (λa.λb.a b) (λy.y) (λz.z)
```

## Система нод

Длля описания выражения строится дерево, которое состоит из множества нод таких типов:

- LambdaSyntaxNode - базовый общий тип для нод
  - AbstractionLambdaSyntaxNode - ноды, которые описывают лямбда выражения
  - ApplicationSyntaxNode - ноды, которые описывают аппликацию
  - ArgumentLambdaSyntaxNode - ноды, которые описывают аргументы абстракции
  - ParameterLambdaSyntaxNode - ноды, которые описывают использование аргумента абстракции
  - ParenthesizedSyntaxNode - ноды, которые описывают взятие в скобки другие ноды

## Known issues

- Нет поддержки сохранения пробелов, они игнорируются и не создаются в синтаксическом дереве. Проблему можно решить снабдив базовый тип полями StartTrivia и EndTrivia, которые будут хранить пробельные символы.