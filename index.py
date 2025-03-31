import copy
import random
import math
import matplotlib.pyplot as plt

def partition(data, lowIndex, highIndex):
    comparisonCount = 0

    pivotValue = data[highIndex]
    smallerIndex = lowIndex - 1

    for currentIndex in range(lowIndex, highIndex):
        comparisonCount += 1

        if data[currentIndex] <= pivotValue:
            smallerIndex += 1
            
            data[smallerIndex], data[currentIndex] = data[currentIndex], data[smallerIndex]

    data[smallerIndex + 1], data[highIndex] = data[highIndex], data[smallerIndex + 1]

    return smallerIndex + 1, comparisonCount

def partition2(data, lowIndex, highIndex):
    comparisonCount = 0

    firstElement = data[lowIndex]
    middleElement = data[(lowIndex + highIndex) // 2]
    lastElement = data[highIndex]

    pivotValue = sorted([firstElement, middleElement, lastElement])[1]

    if pivotValue == firstElement:
        pivotIndex = lowIndex
    elif pivotValue == middleElement:
        pivotIndex = (lowIndex + highIndex) // 2
    else:
        pivotIndex = highIndex

    data[pivotIndex], data[highIndex] = data[highIndex], data[pivotIndex]

    smallerIndex = lowIndex - 1

    for currentIndex in range(lowIndex, highIndex):
        comparisonCount += 1

        if data[currentIndex] <= pivotValue:
            smallerIndex += 1

            data[smallerIndex], data[currentIndex] = data[currentIndex], data[smallerIndex]

    data[smallerIndex + 1], data[highIndex] = data[highIndex], data[smallerIndex + 1]

    return smallerIndex + 1, comparisonCount

def partition3(data, lowIndex, highIndex):
    comparisonCount = 3

    if data[lowIndex] > data[highIndex]:
        data[lowIndex], data[highIndex] = data[highIndex], data[lowIndex]
    if data[lowIndex + 1] > data[highIndex]:
        data[lowIndex + 1], data[highIndex] = data[highIndex], data[lowIndex + 1]
    if data[lowIndex] > data[lowIndex + 1]:
        data[lowIndex], data[lowIndex + 1] = data[lowIndex + 1], data[lowIndex]

    lessBoundary = lowIndex + 2
    currentIndex = lowIndex + 2
    greaterPointer = highIndex - 1

    greatBoundary = highIndex - 1

    lowPivot = data[lowIndex]
    midPivot = data[lowIndex + 1]
    highPivot = data[highIndex]

    while currentIndex <= greaterPointer:
        while currentIndex <= greaterPointer and data[currentIndex] < midPivot:
            comparisonCount += 1

            if data[currentIndex] < lowPivot:
                data[lessBoundary], data[currentIndex] = data[currentIndex], data[lessBoundary]
                
                lessBoundary += 1
            currentIndex += 1
        while currentIndex <= greaterPointer and data[greaterPointer] > midPivot:
            comparisonCount += 1

            if data[greaterPointer] > highPivot:
                data[greaterPointer], data[greatBoundary] = data[greatBoundary], data[greaterPointer]
                
                greatBoundary -= 1
            greaterPointer -= 1

        if currentIndex <= greaterPointer:
            comparisonCount += 1

            if data[currentIndex] > highPivot:
                comparisonCount += 1

                if data[greaterPointer] < lowPivot:
                    data[currentIndex], data[lessBoundary] = data[lessBoundary], data[currentIndex]
                    data[lessBoundary], data[greaterPointer] = data[greaterPointer], data[lessBoundary]
                    lessBoundary += 1
                else:
                    data[currentIndex], data[greaterPointer] = data[greaterPointer], data[currentIndex]
                data[greaterPointer], data[greatBoundary] = data[greatBoundary], data[greaterPointer]

                currentIndex += 1
                greaterPointer -= 1
                greatBoundary -= 1
            else:
                comparisonCount += 1

                if data[greaterPointer] < lowPivot:
                    data[currentIndex], data[lessBoundary] = data[lessBoundary], data[currentIndex]
                    data[lessBoundary], data[greaterPointer] = data[greaterPointer], data[lessBoundary]

                    lessBoundary += 1
                else:
                    data[currentIndex], data[greaterPointer] = data[greaterPointer], data[currentIndex]
                    
                currentIndex += 1
                greaterPointer -= 1

    lessBoundary -= 1
    currentIndex -= 1

    greaterPointer += 1
    greatBoundary += 1

    data[lowIndex + 1], data[lessBoundary] = data[lessBoundary], data[lowIndex + 1]
    data[lessBoundary], data[currentIndex] = data[currentIndex], data[lessBoundary]

    lessBoundary -= 1

    data[lowIndex], data[lessBoundary] = data[lessBoundary], data[lowIndex]
    data[highIndex], data[greatBoundary] = data[greatBoundary], data[highIndex]

    return lessBoundary, currentIndex, greatBoundary, comparisonCount

def quickSort(data, lowIndex, highIndex):
    comparisonCount = 0

    if lowIndex < highIndex:
        pivotIndex, comps = partition(data, lowIndex, highIndex)
        comparisonCount += comps
        comparisonCount += quickSort(data, lowIndex, pivotIndex - 1)
        comparisonCount += quickSort(data, pivotIndex + 1, highIndex)

    return comparisonCount

def quickSort2(data, lowIndex, highIndex):
    comparisonCount = 0

    if lowIndex < highIndex:
        if highIndex - lowIndex + 1 > 3:
            pivotIndex, comps = partition2(data, lowIndex, highIndex)
            comparisonCount += comps
            comparisonCount += quickSort2(data, lowIndex, pivotIndex - 1)
            comparisonCount += quickSort2(data, pivotIndex + 1, highIndex)
        else:
            for index in range(lowIndex + 1, highIndex + 1):
                keyValue = data[index]
                position = index - 1

                while position >= lowIndex and data[position] > keyValue:
                    comparisonCount += 1
                    data[position + 1] = data[position]
                    position -= 1

                comparisonCount += 1

                data[position + 1] = keyValue

    return comparisonCount

def quickSort3(data, lowIndex, highIndex):
    comparisonCount = 0

    if lowIndex < highIndex:
        if highIndex - lowIndex + 1 > 3:
            lowPart, midPart, highPart, comps = partition3(data, lowIndex, highIndex)
            comparisonCount += comps

            comparisonCount += quickSort3(data, lowIndex, lowPart - 1)
            comparisonCount += quickSort3(data, lowPart + 1, midPart - 1)
            comparisonCount += quickSort3(data, midPart + 1, highPart - 1)
            comparisonCount += quickSort3(data, highPart + 1, highIndex)
        else:
            for index in range(lowIndex + 1, highIndex + 1):
                keyValue = data[index]
                position = index - 1
                
                while position >= lowIndex and data[position] > keyValue:
                    comparisonCount += 1
                    data[position + 1] = data[position]
                    position -= 1
                    
                comparisonCount += 1
                data[position + 1] = keyValue

    return comparisonCount

def runExperiment(maxSize):
    sizesList = list(range(1, maxSize + 1))

    comparisonsQS = []
    comparisonsQS2 = []
    comparisonsQS3 = []

    for size in sizesList:
        randomList = random.sample(range(1, maxSize * 2), size)

        listForQS = copy.deepcopy(randomList)
        listForQS2 = copy.deepcopy(randomList)
        listForQS3 = copy.deepcopy(randomList)

        comparisonsQS.append(quickSort(listForQS, 0, len(listForQS) - 1))
        comparisonsQS2.append(quickSort2(listForQS2, 0, len(listForQS2) - 1))
        comparisonsQS3.append(quickSort3(listForQS3, 0, len(listForQS3) - 1))

    return sizesList, comparisonsQS, comparisonsQS2, comparisonsQS3

def plotExperiment(sizesList, qsResults, qs2Results, qs3Results, plotTitle):
    plt.figure(figsize=(8, 6))
    plt.plot(sizesList, qsResults, label='quickSort', marker='o')
    plt.plot(sizesList, qs2Results, label='quickSort2', marker='s')
    plt.plot(sizesList, qs3Results, label='quickSort3', marker='^')
    plt.title(plotTitle)
    plt.xlabel('Розмір масиву')
    plt.ylabel('Кількість порівнянь')
    plt.legend()
    plt.grid(True)
    plt.tight_layout()
    
    plt.show()

sizes10, qsResults10, qs2Results10, qs3Results10 = runExperiment(10)
plotExperiment(sizes10, qsResults10, qs2Results10, qs3Results10,
               'Залежність кількості порівнянь від розмірності масиву (1-10)')

sizes100, qsResults100, qs2Results100, qs3Results100 = runExperiment(100)
plotExperiment(sizes100, qsResults100, qs2Results100, qs3Results100,
               'Залежність кількості порівнянь від розмірності масиву (1-100)')

sizes1000, qsResults1000, qs2Results1000, qs3Results1000 = runExperiment(1000)
plotExperiment(sizes1000, qsResults1000, qs2Results1000, qs3Results1000,
               'Залежність кількості порівнянь від розмірності масиву (1-1000)')

sizes5000, qsResults5000, qs2Results5000, qs3Results5000 = runExperiment(5000)
plotExperiment(sizes5000, qsResults5000, qs2Results5000, qs3Results5000,
               'Залежність кількості порівнянь від розмірності масиву (1-5000)')

sizes10000, qsResults10000, qs2Results10000, qs3Results10000 = runExperiment(10000)
plotExperiment(sizes10000, qsResults10000, qs2Results10000, qs3Results10000,
               'Залежність кількості порівнянь від розмірності масиву (1-10000)')