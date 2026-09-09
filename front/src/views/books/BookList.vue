<template>
  <div class="book-list">
    <div v-if="error" class="error-message">
      {{ error }}
    </div>

    <div v-if="loading" class="loading">
      Carregando...
    </div>

    <div v-else>
      <div class="books-container">
        <BookCard 
          v-for="book in books" 
          :key="book.id" 
          :book="book"
        />
      </div>

      <div v-if="books.length === 0" class="no-books">
        Nenhum livro encontrado.
      </div>

      <Pagination
        :current-page="currentPage"
        :is-last-page="books.length < pageSize"
        :loading="loading"
        @page-change="changePage"
      />
    </div>
  </div>
</template>

<script>
import BookCard from '@/components/books/BookCard.vue'
import Pagination from '@/components/common/Pagination.vue'
import { bookService } from '@/services/api'

export default {
  components: {
    BookCard,
    Pagination
  },
  data() {
    return {
      books: [],
      currentPage: 1,
      pageSize: 10,
      loading: false,
      error: null
    }
  },
  methods: {
    async fetchBooks() {
      this.loading = true
      this.error = null
      try {
        this.books = await bookService.getBooks(this.currentPage, this.pageSize)
      } catch (error) {
        console.error('Erro ao buscar livros:', error)
        this.error = 'Erro ao carregar os livros. Por favor, tente novamente.'
      } finally {
        this.loading = false
      }
    },
    changePage(page) {
      this.currentPage = page
      this.fetchBooks()
    }
  },
  mounted() {
    this.fetchBooks()
  }
}
</script>

<style scoped>
.book-list {
  padding: 2rem;
  max-width: 1200px;
  margin: 0 auto;
}

.books-container {
  display: grid;
  grid-template-columns: repeat(auto-fill, minmax(300px, 1fr));
  gap: 1.5rem;
  margin-bottom: 2rem;
}

.error-message {
  color: red;
  text-align: center;
  padding: 1rem;
  margin-bottom: 1rem;
  background-color: #ffebee;
  border-radius: 4px;
}

.loading {
  text-align: center;
  padding: 2rem;
  color: #666;
}

.no-books {
  text-align: center;
  padding: 2rem;
  color: #666;
}
</style> 