const API_URL = '/api'

export const bookService = {
  async getBooks(page, pageSize) {
    const response = await fetch(
      `${API_URL}/BookControllerr?page=${page}&pageSize=${pageSize}`
    )
    if (!response.ok) {
      throw new Error(`HTTP error! status: ${response.status}`)
    }
    return response.json()
  }
} 