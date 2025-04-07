export function initializeChat(dotNetHelper, element) {
    return {
        scrollToBottom: () => {
            element.scrollTop = element.scrollHeight;
        },
        focusInput: (inputElement) => {
            inputElement.focus();
        }
    };
}