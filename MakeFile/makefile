# say_hello:
# 	@echo "Hello Pankaj"
# 	touch file-{1..1000}.txt

.PHONY: all say_hello generate clean

all: say_hello generate

say_hello:
	@echo "Hello World"

generate:
	@echo "Creating empty text files..."
	touch file-{1..10}.txt

clean:
	@echo "Cleaning up..."
	rm *.txt

UNIWEBVIEW_SRC_DIR = $/UniWebView/Source/Android
.PHONY: restore-uniwebview-src
restore-uniwebview-src:
	@echo "p4 sync helpshift library at path $(UNIWEBVIEW_SRC_DIR)"