export const getTypePath = (type) => {
  const typeMap = {
    ARTICLE_NOTE: '/article/note',
    ARTICLE_PROJECT: '/article/project',
    ARTICLE_BUG: '/article/bug',
    MEDIA_BOOK: '/media/book',
    MEDIA_PHOTO: '/media/photo',
    MEDIA_VIDEO: '/media/video',
    MEDIA_MUSIC: '/media/music',
    TOOL: '/tool',
    BOARD: '/board',
  }
  return typeMap[type] || '/article/note'
}
