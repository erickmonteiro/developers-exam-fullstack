const API_URL = "/api"

export const bookService = {
	async getBooks(page, pageSize) {
		const response = await fetch(
			`${API_URL}/books?page=${page}&pageSize=${pageSize}`
		)

		if( !response.ok )
		{
			throw new Error(`HTTP error! status: ${response.status}`)
		}

		const result = await response.json()

		return result.data;
	}
}