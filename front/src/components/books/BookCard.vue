<template>
  <div class="book-card">
    <h2>{{ book.title }}</h2>
    <p><strong>Autor:</strong> {{ book.author }}</p>
    <p>{{ book.description }}</p>
  </div>
</template>

<script>
export default {
  props: {
    book: {
      type: Object,
      required: true
    }
  }
}
</script>

<style scoped>
.book-card {
  border: 1px solid #ddd;
  border-radius: 8px;
  padding: 1rem;
  background-color: white;
  box-shadow: 0 2px 4px rgba(0, 0, 0, 0.1);
}

.book-card h2 {
  color: #0c66a3;
  margin-top: 0;
}
</style> 