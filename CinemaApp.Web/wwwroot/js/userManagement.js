function setRoleValue(userId, inputId) {
    var dropdown = document.getElementById('roleDropdown-' + userId);
    var input = document.getElementById(inputId + '-' + userId);
    input.value = dropdown.value;
}