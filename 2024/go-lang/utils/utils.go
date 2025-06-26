package utils

import (
	"log"
	"os"
	"path/filepath"
)

const InputsDirectory string = "inputs"

func ReadInputFile(fileName string) []byte {
	filePath := filepath.Join(InputsDirectory, fileName)
	data, err := os.ReadFile(filePath)
	if err != nil {
		log.Fatal(err)
	}

	return data
}
