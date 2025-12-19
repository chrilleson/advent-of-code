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

	fmt.Println("totalZeroCount", PartOne(cleanLines))
}

func PartOne(instructions []string) int {
	const initDialPosition = 50
	totalZeroCount := 0
	currentPosition := initDialPosition

	for _, instruction := range instructions {
		direction := instruction[:1]
		distance := instruction[1:]
		distanceNum, err := strconv.Atoi(distance)
		if err != nil {
			log.Fatal(err)
		}

		switch direction {
		case "R":
			currentPosition = currentPosition + distanceNum
		case "L":
			currentPosition = currentPosition - distanceNum
		}

		currentPosition = ((currentPosition % 100) + 100) % 100
		if currentPosition == 0 {
			totalZeroCount++
		}
	}

	return totalZeroCount
}
