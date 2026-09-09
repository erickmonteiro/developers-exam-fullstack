<template>
  <div class="pagination">
    <button :disabled="currentPage === 1 || loading" @click="$emit('page-change', currentPage - 1)">
      Anterior
    </button>
    <span>Página {{ currentPage }}</span>
    <button :disabled="isLastPage || loading" @click="$emit('page-change', currentPage + 1)">
      Próxima
    </button>
  </div>
</template>

<script>
export default {
  props: {
    currentPage: {
      type: Number,
      required: true
    },
    isLastPage: {
      type: Boolean,
      required: true
    },
    loading: {
      type: Boolean,
      default: false
    }
  }
}
</script>

<style scoped>
.pagination {
  display: flex;
  justify-content: center;
  align-items: center;
  gap: 1rem;
}

.pagination button {
  padding: 0.5rem 1rem;
  background-color: #0c66a3;
  color: white;
  border: none;
  border-radius: 4px;
  cursor: pointer;
}

.pagination button:disabled {
  background-color: #ccc;
  cursor: not-allowed;
}

.pagination span {
  font-weight: bold;
}
</style> 