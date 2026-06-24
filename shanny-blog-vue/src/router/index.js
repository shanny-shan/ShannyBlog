import { createRouter, createWebHistory } from 'vue-router'
const Layout = () => import('@/views/Layout/LayoutIndex.vue')
const Home = () => import('@/views/Home/HomeIndex.vue')
const Note = () => import('@/views/Note/NoteIndex.vue')
const NoteDetail = () => import('@/views/Note/NoteDetail.vue')
const Project = () => import('@/views/Project/ProjectIndex.vue')
const ProjectDetail = () => import('@/views/Project/ProjectDetail.vue')
const Tool = () => import('@/views/Tool/ToolIndex.vue')
const Tag = () => import('@/views/Tag/TagIndex.vue')
// const Media = () => import('@/views/Media/MediaIndex.vue')
// const Bug = () => import('@/views/Bug/BugIndex.vue')
// const Board = () => import('@/views/Board/BoardIndex.vue')

const routes = [
  {
    path: '',
    component: Layout,
    children: [
      {
        path: '',
        component: Home,
        meta: { id: 'home' },
      },
      {
        path: '/article/note',
        component: Note,
        meta: { id: 'note' },
      },
      {
        path: '/article/note/:id',
        component: NoteDetail,
        meta: { id: 'note' },
      },
      {
        path: '/article/project',
        component: Project,
        meta: { id: 'project' },
      },
      {
        path: '/article/project/:id',
        component: ProjectDetail,
        meta: { id: 'project' },
      },
      {
        path: '/tool',
        component: Tool,
        meta: { id: 'tool' },
      },
      {
        path: '/tag/:tagId/:tagName',
        component: Tag,
        meta: { id: 'tag' },
      },
      // {
      //   path: '/media/:type',
      //   component: Media,
      // },
      // {
      //   path: '/article/bug',
      //   component: Bug,
      // },
      // {
      //   path: '/board',
      //   component: Board,
      // },
    ],
  },
]
const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes,
})
router.beforeEach(async (to, from, next) => {
  const storeModule = await import('@/stores')
  const useSiteStore = storeModule.useSiteStore
  const siteStore = useSiteStore()

  if (to.meta.id) {
    siteStore.curHref = to.meta.id
  }
  next()
})

export default router
