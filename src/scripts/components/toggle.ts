export const setUpToggleEventListeners = () => {
  const toggleButtons = document.querySelectorAll<HTMLElement>(
    '[data-event="toggle-element"]'
  );

  toggleButtons.forEach((toggleButton) => {
    toggleButton?.addEventListener("click", () => {
      const target = toggleButton.dataset.toggleTarget;
      const targetElement = document.querySelector<HTMLElement>(
        `[data-container="${target}"]`
      );
      if (!targetElement) return;

      targetElement.classList.toggle("open");
    });
  });
};
