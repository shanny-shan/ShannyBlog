 const months = [
    'January', 'February', 'March', 'April', 'May', 'June',
    'July', 'August', 'September', 'October', 'November', 'December'
  ]
const formatDateTime = (time) => {
  if (!time) return ''

  const [date] = time.split('T')
  const [year, month, day] = date.split('-')
  return `${months[Number(month) - 1]} ${Number(day)}, ${year}`
}
export { formatDateTime }
