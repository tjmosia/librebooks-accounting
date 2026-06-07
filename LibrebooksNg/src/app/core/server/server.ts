export const server = (function () {
    const apiUrl = "https://localhost:5262"
    return {
        getBaseApiUrl: apiUrl,
        getFullApiUrl: (uri: string) => apiUrl.concat(uri)
    }
})()