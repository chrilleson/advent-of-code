package utils

import (
	"log"
	"os"
	"path/filepath"
)

func ReadInputFile(fileName string) []byte {
	filePath := filepath.Join("inputs", fileName)
	data, err := os.ReadFile(filePath)
	if err != nil {
		log.Fatal(err)
	}

	return data
}
