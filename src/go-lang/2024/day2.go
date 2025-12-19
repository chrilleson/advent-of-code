package year2024

import (
	"advent-of-code/utils"
	"fmt"
	"math"
	"strconv"
	"strings"
)

func Day2() {
	data := utils.ReadInputFile("2024", "day2")
	reports := extractReports(data)

	safeReports := 0
	for _, report := range reports {
		if isSafe(report) {
			safeReports++
		}
	}

	fmt.Println("Safe reports:", safeReports)

	safeWithDampener := 0
	for _, report := range reports {
		if withDampener(report) {
			safeWithDampener++
		}
	}
	fmt.Println("Dampened safe reports:", safeWithDampener)
}

func extractReports(data []byte) [][]int {
	lines := strings.Split(strings.TrimSpace(string(data)), "\n")
	for i, line := range lines {
		lines[i] = strings.TrimSpace(line)
	}
	reports := make([][]int, 0)
	for _, line := range lines {
		report := make([]int, 0)
		numbers := strings.Split(line, " ")
		for _, numStr := range numbers {
			num, err := strconv.Atoi(numStr)
			if err != nil {
				fmt.Printf("Error converting string to int: %v\n", err)
				continue
			}
			report = append(report, num)
		}
		reports = append(reports, report)
	}

	return reports
}

func isSafe(report []int) bool {
	isAscending := func(report []int) bool {
		for i := 1; i < len(report); i++ {
			if report[i] <= report[i-1] {
				return false
			}
		}
		return true
	}

	isDescending := func(report []int) bool {
		for i := 1; i < len(report); i++ {
			if report[i] >= report[i-1] {
				return false
			}
		}
		return true
	}

	hasValidDifference := func(report []int) bool {
		for i := 1; i < len(report); i++ {
			difference := math.Abs(float64(report[i] - report[i-1]))
			if difference < 1 || difference > 3 {
				return false
			}
		}
		return true
	}

	return (isAscending(report) || isDescending(report)) && hasValidDifference(report)
}

func withDampener(report []int) bool {
	if isSafe(report) {
		return true
	}

	for i := range report {
		modified := append([]int{}, report[:i]...)
		modified = append(modified, report[i+1:]...)
		if isSafe(modified) {
			return true
		}
	}
	return false
}
