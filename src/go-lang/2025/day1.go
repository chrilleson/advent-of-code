package year2025

import (
	"advent-of-code/utils"
	"fmt"
	"log"
	"strconv"
	"strings"
)

func Day1() {
	data := utils.ReadInputFile("2025", "day1")
	cleanLines := strings.Split(strings.TrimSpace(string(data)), "\n")

	for i, line := range cleanLines {
		cleanLines[i] = strings.TrimSpace(line)
	}

	partOneAnswer, partTwoAnswer := PartOne(cleanLines)

	fmt.Println("Part 1 Answer: ", partOneAnswer)
	fmt.Println("Part 2 Answer: ", partTwoAnswer)
}

const initDialPosition = 50

func PartOne(instructions []string) (int, int) {
	partOneCount := 0 // Part 1: only count landing on 0
	partTwoCount := 0 // Part 2: count all passes through 0
	currentPosition := initDialPosition

	for _, instruction := range instructions {
		direction := instruction[:1]
		distance := instruction[1:]
		distanceNum, err := strconv.Atoi(distance)
		if err != nil {
			log.Fatal(err)
		}

		startPosition := currentPosition
		crossedZero := false
		switch direction {
		case "R":
			currentPosition = ((currentPosition+distanceNum)%100 + 100) % 100
			crossedZero = currentPosition < startPosition
		case "L":
			currentPosition = ((currentPosition-distanceNum)%100 + 100) % 100
			crossedZero = currentPosition > startPosition
		}

		// Part 1: Count only when landing exactly on 0
		if currentPosition == 0 {
			partOneCount++
		}

		// Part 2: Count all passes through 0
		completeCrosses := distanceNum / 100
		partialCrosses := 0
		if (crossedZero || currentPosition == 0) && startPosition != 0 {
			partialCrosses = 1
		}
		partTwoCount += completeCrosses + partialCrosses
	}

	return partOneCount, partTwoCount
}
