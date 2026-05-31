const objectToFormData = (data) => {
  const formData = new FormData()
  for (const key in data) {
    const value = data[key]
    if (value !== null && value !== undefined) {
      formData.append(key, value)
    }
  }
  return formData
}

export { objectToFormData }
