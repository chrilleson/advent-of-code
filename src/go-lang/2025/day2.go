package year2025

import (
	"fmt"
	"strconv"
	"strings"

	"advent-of-code/utils"
)

func Day2() {
	inputs := string(utils.ReadInputFile("2025", "day2"))

	ranges := parseRanges(inputs)
	fmt.Printf("Part 1: %d\n", sumInvalidIDs(ranges, isInvalidIDPart1))
	fmt.Printf("Part 2: %d\n", sumInvalidIDs(ranges, isInvalidIDPart2))
}

func parseRanges(input string) [][2]int {
	ranges := [][2]int{}
	for _, lines := range strings.Split(strings.TrimSpace(input), ",") {
		parts := strings.Split(lines, "-")
		start, _ := strconv.Atoi(parts[0])
		end, _ := strconv.Atoi(parts[1])
		ranges = append(ranges, [2]int{start, end})
	}

	return ranges
}

func isInvalidIDPart1(n int) bool {
	s := strconv.Itoa(n)
	if len(s)%2 != 0 {
		return false
	}
	mid := len(s) / 2
	return s[:mid] == s[mid:]
}

func isInvalidIDPart2(n int) bool {
	s := strconv.Itoa(n)
	length := len(s)
	for chunkSize := 1; chunkSize <= length/2; chunkSize++ {
		if length%chunkSize != 0 {
			continue
		}
		chunk := s[:chunkSize]
		valid := true
		for i := chunkSize; i < length; i += chunkSize {
			if s[i:i+chunkSize] != chunk {
				valid = false
				break
			}
		}
		if valid {
			return true
		}
	}
	return false
}

func sumInvalidIDs(ranges [][2]int, check func(int) bool) int {
	sum := 0
	for _, r := range ranges {
		for id := r[0]; id <= r[1]; id++ {
			if check(id) {
				sum += id
			}
		}
	}
	return sum
}
