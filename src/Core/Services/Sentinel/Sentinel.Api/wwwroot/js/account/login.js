document.addEventListener('DOMContentLoaded', () => {  
    const togglePassword = document.getElementById('togglePassword');  
    const passwordInput = document.getElementById('Password');  
    const passwordVisibleInput = document.getElementById('PasswordVisible');  
  
    if (!togglePassword || !passwordInput || !passwordVisibleInput) {  
        return; // page might not have these elements  
    }  
  
    // ensure this control doesn't submit the form  
    if (togglePassword.tagName === 'BUTTON') {  
        togglePassword.setAttribute('type', 'button');  
    }  
  
    const syncUi = (visible) => {  
        passwordInput.type = visible ? 'text' : 'password';  
        togglePassword.setAttribute('aria-label', visible ? 'Ocultar senha' : 'Mostrar senha');  
        togglePassword.setAttribute('aria-pressed', String(visible));  
    };  
  
    const initialVisible = passwordVisibleInput.value.toLowerCase() === 'true';  
    syncUi(initialVisible);  
  
    togglePassword.addEventListener('click', (e) => {  
        e.preventDefault();  
        const isVisible = passwordVisibleInput.value.toLowerCase() === 'true';  
        const newVisible = !isVisible;  
        passwordVisibleInput.value = String(newVisible);  
        syncUi(newVisible);  
    });  
});  