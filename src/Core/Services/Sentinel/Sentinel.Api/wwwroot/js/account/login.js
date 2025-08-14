const togglePassword = document.getElementById('togglePassword');
const passwordInput = document.getElementById('Password');
const passwordVisibleInput = document.getElementById('PasswordVisible');

togglePassword.addEventListener('click', () => {
    const isVisible = passwordVisibleInput.value.toLowerCase() == 'true'
    passwordVisibleInput.value = !isVisible;
    const newVisible = !isVisible;

    passwordInput.type = newVisible ? 'text' : 'password';
    togglePassword.setAttribute('aria-label', newVisible ? 'Ocultar senha' : 'Mostrar senha');
});

document.addEventListener('DOMContentLoaded', () => {
    const isVisible = passwordVisibleInput.value.toLowerCase() == 'true'

    passwordInput.type = isVisible ? 'text' : 'password';
    togglePassword.setAttribute('aria-label', isVisible ? 'Ocultar senha' : 'Mostrar senha');
});