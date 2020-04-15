library(caTools)
rsq <- function (x, y) cor(x, y) ^ 2

dataset <- read.table("datasets/weight-height.csv", header=TRUE, sep=",", nrows=100)
isTrain = sample.split(dataset, SplitRatio = 0.3)
train = subset(dataset, isTrain == TRUE)
test = subset(dataset, isTrain == FALSE)

relation <- lm(Height ~ Weight, data =train)
predictions <- predict(relation, newdata =test)

sprintf("MSE=%0.2f", mean((test$Height - predictions) ^ 2))
sprintf("R2_score=%0.2f", rsq(test$Height, predictions))

plot(dataset[,2], dataset[,3],
    col = "blue", abline(relation),
    ylab="Height", xlab="Weight")