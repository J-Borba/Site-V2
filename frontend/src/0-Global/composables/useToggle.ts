import { ref } from 'vue';

export function useSingleToggle<T>() {
  const active = ref<T | null>(null);
  function toggle(val: T) {
    active.value = (active.value === val ? null : val) as T | null;
  }
  return { active, toggle };
}

export function useMultiToggle() {
  const expanded = ref<Set<number>>(new Set());
  function toggle(index: number) {
    const next = new Set(expanded.value);
    next.has(index) ? next.delete(index) : next.add(index);
    expanded.value = next;
  }
  return { expanded, toggle };
}
