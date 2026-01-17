.PHONY: install lint test run

install:
	pip install -r requirements.txt

lint:
	python -m compileall src

test:
	pytest -q

run:
	PYTHONPATH=src python -m main --config config.yml
