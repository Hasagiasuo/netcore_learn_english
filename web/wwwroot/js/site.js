let health = 5;
let wordToCollect = '';
let currentLetterIndex = 0;
let generationCount = 0;
let gameContainer = document.querySelector('.game-container');
let healthDisplay = document.getElementById('health');
let scoreDisplay = document.getElementById('score');
let startScreen = document.querySelector('.start-screen');
let endScreen = document.querySelector('.end-screen');
let endMessage = document.getElementById('end-message');
let startBtn = document.getElementById('start-btn');
let restartBtn = document.getElementById('restart-btn');
let wordInput = document.getElementById('word-input');
let loaderWrapper = document.querySelector('.loader-wrapper');

function set_target_word(new_word) {
    wordToCollect = new_word;
    console.log("change");
    document.getElementById("word-input").value = new_word;
}

// Функція для отримання слова з сервера
async function fetchWord() {
    try {
        lwordToCollect = new_word;
        console.log("change");
        document.getElementById("word-input").value = new_word;
    } catch (error) {
        console.error('Помилка завантаження слова:', error);
        return 'apple';
    }
}

// Функція для старту гри
async function initializeGame() {
    wordToCollect = await fetchWord();
    if (wordToCollect.length === 0) {
        alert('Не вдалося отримати слово.');
        return;
    }

    wordInput.value = wordToCollect; // Встановлює слово в input для зручності
    resetGame();
    loaderWrapper.style.display = 'none';
    document.querySelector('.container').style.display = 'flex';
    startGame();
}

// Функція для старту гри
startBtn.addEventListener('click', () => {
    wordToCollect = wordInput.value.trim();
    if (wordToCollect.length === 0) {
        alert('Будь ласка, введіть слово.');
        return;
    }
    resetGame();
    startScreen.style.display = 'none';
    gameContainer.style.display = 'flex';
    startGame();
});

// Функція для рестарту гри
restartBtn.addEventListener('click', () => {
    endScreen.style.display = 'none';
    startScreen.style.display = 'flex';
});

// Ініціалізація гри при завантаженні сторінки
window.onload = initializeGame;

function startGame() {
    scoreDisplay.textContent = `Слово: ${"_ ".repeat(wordToCollect.length)}`;
    createObject();
}

// Функція для створення об'єктів
function createObject() {
    let object = document.createElement('div');
    object.classList.add('object');

    generationCount++;

    let letterToShow;
    if (generationCount % 3 === 0 && currentLetterIndex < wordToCollect.length) {
        letterToShow = wordToCollect[currentLetterIndex].toUpperCase();
    } else {
        letterToShow = String.fromCharCode(65 + Math.floor(Math.random() * 26));
    }

    object.textContent = letterToShow;

    object.style.top = '0px';
    object.style.left = Math.random() * (gameContainer.clientWidth - 30) + 'px';
    gameContainer.appendChild(object);

    let fallInterval = setInterval(() => {
        let currentTop = parseInt(object.style.top);
        object.style.top = currentTop + 2 + 'px';

        if (currentTop > gameContainer.clientHeight) {
            clearInterval(fallInterval);
            gameContainer.removeChild(object);
        }
    }, 20);

    object.addEventListener('click', () => {
        if (letterToShow === wordToCollect[currentLetterIndex].toUpperCase()) {
            currentLetterIndex++;
            updateScore();

            if (currentLetterIndex === wordToCollect.length) {
                gameOver(true);
            }
        } else {
            health--;
            updateHealth();

            if (health <= 0) {
                gameOver(false);
            }
        }

        clearInterval(fallInterval);
        gameContainer.removeChild(object);
    });

    if (health > 0 && currentLetterIndex < wordToCollect.length) {
        setTimeout(createObject, 2000);
    }
}

// Оновлення відображення здоров'я і очок
function updateHealth() {
    healthDisplay.textContent = `Здоров'я: ${health}`;
}

function updateScore() {
    let collected = wordToCollect.slice(0, currentLetterIndex);
    let remaining = "_ ".repeat(wordToCollect.length - currentLetterIndex);
    scoreDisplay.textContent = `Слово: ${collected + remaining}`;
}

// Завершення гри
function gameOver(won) {
    gameContainer.style.display = 'none';
    endScreen.style.display = 'flex';

    if (won) {let health = 5;
        let wordToCollect = '';
        let currentLetterIndex = 0;
        let generationCount = 0;
        let gameContainer = document.querySelector('.game-container');
        let healthDisplay = document.getElementById('health');
        let scoreDisplay = document.getElementById('score');
        let startScreen = document.querySelector('.start-screen');
        let endScreen = document.querySelector('.end-screen');
        let endMessage = document.getElementById('end-message');
        let startBtn = document.getElementById('start-btn');
        let restartBtn = document.getElementById('restart-btn');
        let fileInput = document.getElementById('file-input');
        let loaderWrapper = document.querySelector('.loader-wrapper');
        
        // Функція для завантаження слова з локального файлу
        function loadWordFromFile(file) {
            return new Promise((resolve, reject) => {
                const reader = new FileReader();
                reader.onload = function(e) {
                    const content = e.target.result.trim();
                    if (content.length > 0) {
                        resolve(content);
                    } else {
                        reject('Файл порожній');
                    }
                };
                reader.onerror = function() {
                    reject('Помилка читання файлу');
                };
                reader.readAsText(file);
            });
        }
        
        // Функція для старту гри
        startBtn.addEventListener('click', async () => {
            const file = fileInput.files[0];
            if (!file) {
                alert('Будь ласка, виберіть файл.');
                return;
            }
        
            try {
                wordToCollect = await loadWordFromFile(file);
                if (wordToCollect.length === 0) {
                    alert('Файл порожній.');
                    return;
                }
                resetGame();
                loaderWrapper.style.display = 'none';
                document.querySelector('.container').style.display = 'flex';
                startGame();
            } catch (error) {
                alert('Помилка: ' + error);
            }
        });
        
        // Функція для рестарту гри
        restartBtn.addEventListener('click', () => {
            endScreen.style.display = 'none';
            startScreen.style.display = 'flex';
        });
        
        function startGame() {
            scoreDisplay.textContent = `Слово: ${"_ ".repeat(wordToCollect.length)}`;
            createObject();
        }
        
        // Функція для створення об'єктів
        function createObject() {
            let object = document.createElement('div');
            object.classList.add('object');
        
            generationCount++;
        
            let letterToShow;
            if (generationCount % 3 === 0 && currentLetterIndex < wordToCollect.length) {
                letterToShow = wordToCollect[currentLetterIndex].toUpperCase();
            } else {
                letterToShow = String.fromCharCode(65 + Math.floor(Math.random() * 26));
            }
        
            object.textContent = letterToShow;
        
            object.style.top = '0px';
            object.style.left = Math.random() * (gameContainer.clientWidth - 30) + 'px';
            gameContainer.appendChild(object);
        
            let fallInterval = setInterval(() => {
                let currentTop = parseInt(object.style.top);
                object.style.top = currentTop + 2 + 'px';
        
                if (currentTop > gameContainer.clientHeight) {
                    clearInterval(fallInterval);
                    gameContainer.removeChild(object);
                }
            }, 20);
        
            object.addEventListener('click', () => {
                if (letterToShow === wordToCollect[currentLetterIndex].toUpperCase()) {
                    currentLetterIndex++;
                    updateScore();
        
                    if (currentLetterIndex === wordToCollect.length) {
                        gameOver(true);
                    }
                } else {
                    health--;
                    updateHealth();
        
                    if (health <= 0) {
                        gameOver(false);
                    }
                }
        
                clearInterval(fallInterval);
                gameContainer.removeChild(object);
            });
        
            if (health > 0 && currentLetterIndex < wordToCollect.length) {
                setTimeout(createObject, 2000);
            }
        }
        
        // Оновлення відображення здоров'я і очок
        function updateHealth() {
            healthDisplay.textContent = `Здоров'я: ${health}`;
        }
        
        function updateScore() {
            let collected = wordToCollect.slice(0, currentLetterIndex);
            let remaining = "_ ".repeat(wordToCollect.length - currentLetterIndex);
            scoreDisplay.textContent = `Слово: ${collected + remaining}`;
        }
        
        // Завершення гри
        function gameOver(won) {
            gameContainer.style.display = 'none';
            endScreen.style.display = 'flex';
        
            if (won) {
                endMessage.textContent = 'Вітаємо! Ви зібрали слово!';
            } else {
                endMessage.textContent = 'Невдача! Спробуйте ще раз!';
            }
        }
        
        // Скидання гри
        function resetGame() {
            health = 5;
            currentLetterIndex = 0;
            generationCount = 0;
            updateHealth();
            updateScore();
        }
        
        endMessage.textContent = 'Вітаємо! Ви зібрали слово!';
    } else {
        endMessage.textContent = 'Невдача! Спробуйте ще раз!';
    }
}

// Скидання гри
function resetGame() {
    health = 5;
    currentLetterIndex = 0;
    generationCount = 0;
    updateHealth();
    updateScore();
}
