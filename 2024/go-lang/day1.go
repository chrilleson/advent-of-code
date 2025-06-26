package main

import (
	"advent-of-code/utils"
	"fmt"
	"log"
	"math"
	"slices"
	"strconv"
	"strings"
)

const TripleSpaceSeparator string = "   "

func Day1() {
	data := utils.ReadInputFile("day1.txt")
	cleanLines := strings.Split(strings.TrimSpace(string(data)), "\n")

	for i, line := range cleanLines {
		cleanLines[i] = strings.TrimSpace(line)
	}

	listOne, listTwo := SplitLists(cleanLines)
	fmt.Println("Total distance:", CalcDistance(listOne, listTwo))
	fmt.Println("Total similarity:", CalcSimilarity(listOne, listTwo))
}

func SplitLists(lines []string) ([]int, []int) {
	listOne := []int{}
	listTwo := []int{}

	for _, line := range lines {
		numbers := strings.Split(line, TripleSpaceSeparator)
		numOne, err := strconv.Atoi(numbers[0])
		if err != nil {
			log.Fatal(err)
		}
		numTwo, err := strconv.Atoi(numbers[1])
		if err != nil {
			log.Fatal(err)
		}
		listOne = append(listOne, numOne)
		listTwo = append(listTwo, numTwo)
	}

	return listOne, listTwo
}

func CalcDistance(listOne []int, listTwo []int) int {
	slices.Sort(listOne)
	slices.Sort(listTwo)

	totalDistance := 0
	for i := range listOne {
		totalDistance += int(math.Abs(float64(listOne[i] - listTwo[i])))
	}

	return totalDistance
}

func CalcSimilarity(listOne []int, listTwo []int) int {
	slices.Sort(listOne)
	slices.Sort(listTwo)

	totalSimilarity := 0
	for _, number := range listOne {
		occurrences := 0
		for _, num := range listTwo {
			if num == number {
				occurrences++
			}
		}
		totalSimilarity += number * occurrences
	}

	return totalSimilarity
}
