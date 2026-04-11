import type { Directive } from 'vue';

const observers = new WeakMap<HTMLElement, IntersectionObserver>();

export const vFadeUp: Directive<HTMLElement> = {
  mounted(el) {
    el.style.opacity = '0';
    el.style.transform = 'translateY(24px)';
    el.style.transition = 'opacity 0.55s ease, transform 0.55s ease';

    const observer = new IntersectionObserver(
      ([entry]) => {
        if (entry.isIntersecting) {
          el.style.opacity = '1';
          el.style.transform = 'translateY(0)';
          observer.disconnect();
          observers.delete(el);
        }
      },
      { threshold: 0.08 },
    );

    observers.set(el, observer);
    observer.observe(el);
  },

  unmounted(el) {
    observers.get(el)?.disconnect();
    observers.delete(el);
  },
};
