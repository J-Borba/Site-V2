<script setup lang="ts">
  defineProps<{
    columns: { key: string; label: string; mono?: boolean; num?: boolean }[];
    items: unknown[];
  }>();
</script>

<template>
  <div class="table-wrap">
    <table class="entity-table">
      <thead>
        <tr>
          <th v-for="col in columns" :key="col.key" :class="{ mono: col.mono, num: col.num }">
            {{ col.label }}
          </th>
          <th class="col-actions"></th>
        </tr>
      </thead>
      <tbody>
        <template v-if="items.length">
          <slot />
        </template>
        <tr v-else class="empty-row">
          <td :colspan="columns.length + 1">
            <slot name="empty">
              <span class="empty-msg">$ nenhum item cadastrado.</span>
            </slot>
          </td>
        </tr>
      </tbody>
    </table>
  </div>
</template>

<style scoped lang="scss">
  .table-wrap {
    width: 100%;
    overflow-x: auto;
    overflow-y: auto;
    max-height: 55dvh;
    border-radius: var(--radius-lg);
    border: 1px solid var(--border-strong);
    background: var(--bg-card);
  }

  .entity-table {
    width: 100%;
    border-collapse: collapse;
    font-size: var(--text-sm);
  }

  thead tr {
    border-bottom: 1px solid var(--border-strong);
    background: rgba(255, 255, 255, 0.03);
  }

  thead {
    position: sticky;
    top: 0;
    z-index: 1;
  }

  th {
    padding: var(--space-3) var(--space-5);
    text-align: left;
    font-family: 'Space Mono', monospace;
    font-size: var(--text-xs);
    color: var(--text-muted);
    font-weight: 400;
    letter-spacing: 0.08em;
    white-space: nowrap;

    &.mono {
      font-family: 'Space Mono', monospace;
    }

    &.num {
      text-align: right;
    }
  }

  .col-actions {
    width: 80px;
  }

  :deep(tbody tr) {
    border-bottom: 1px solid var(--border);
    transition: background 150ms ease;

    &:last-child {
      border-bottom: none;
    }

    &:hover {
      background: var(--brand-dim);
    }
  }

  :deep(td) {
    padding: var(--space-3) var(--space-5);
    color: var(--text-primary);
    white-space: nowrap;

    &.mono {
      font-family: 'Space Mono', monospace;
      font-size: var(--text-xs);
    }

    &.num {
      font-family: 'Space Mono', monospace;
      font-size: var(--text-xs);
      text-align: right;
    }
  }

  .empty-row td {
    padding: var(--space-10) var(--space-5);
    text-align: center;
  }

  .empty-msg {
    font-family: 'Space Mono', monospace;
    font-size: var(--text-sm);
    color: var(--text-muted);
  }

  :deep(.td-actions) {
    display: flex;
    gap: var(--space-2);
    justify-content: flex-end;
  }

  :deep(.action-btn) {
    display: flex;
    align-items: center;
    justify-content: center;
    width: 30px;
    height: 30px;
    border-radius: var(--radius-sm);
    color: var(--text-muted);
    background: transparent;
    border: 1px solid transparent;
    transition:
      color 150ms ease,
      border-color 150ms ease,
      background 150ms ease;

    &:hover {
      color: var(--brand);
      border-color: var(--brand-border);
      background: var(--brand-dim);
    }

    &.danger:hover {
      color: var(--error);
      border-color: var(--error-border);
      background: var(--error-dim);
    }
  }
</style>
