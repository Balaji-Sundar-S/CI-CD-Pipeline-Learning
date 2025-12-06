document.getElementById("calcBtn").addEventListener("click", calculate);

// Add Enter key support
document.querySelectorAll('input[type="number"]').forEach(input => {
        input.addEventListener('keypress', function (e) {
            if (e.key === 'Enter') {
                calculate();
            }
        });
});

    function calculate() {
    const aVal = document.getElementById("numA").value;
    const bVal = document.getElementById("numB").value;
    const op = document.getElementById("operation").value;
    const a = parseFloat(aVal);
    const b = parseFloat(bVal);
    const resultEl = document.getElementById("result");
    const resultContainer = document.querySelector('.result-container');
    const calcBtn = document.getElementById("calcBtn");

    // Basic input validation
    if (isNaN(a) || isNaN(b)) {
        resultEl.textContent = "Invalid Input";
    resultEl.style.color = "#ff4757";
    resultContainer.classList.add('show');
    return;
    }

    // Show loading state
    calcBtn.disabled = true;
    calcBtn.classList.add('loading');
    calcBtn.textContent = "Calculating...";

    const form = new URLSearchParams();
    form.append("a", a);
    form.append("b", b);
    form.append("operation", op);

    fetch("/Calculator/Calculate", {
        method: "POST",
    headers: {"Content-Type": "application/x-www-form-urlencoded" },
    body: form.toString()
    })
    .then(res => res.json())
    .then(data => {
        if (data.success) {
        resultEl.style.color = "white";
    resultEl.textContent = data.result;
    resultContainer.classList.add('show');
        } else {
        resultEl.style.color = "#ff4757";
    resultEl.textContent = data.message || "Error occurred";
    resultContainer.classList.add('show');
        }
    })
    .catch(err => {
        resultEl.style.color = "#ff4757";
    resultEl.textContent = "Request failed: " + err.message;
    resultContainer.classList.add('show');
    })
    .finally(() => {
        // Remove loading state
        calcBtn.disabled = false;
    calcBtn.classList.remove('loading');
    calcBtn.textContent = "Calculate";
    });
}