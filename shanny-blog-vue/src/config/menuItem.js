import { translate } from '@/config/translate'
import { useLanguageStore } from '@/stores/modules/language'
export const menuItems = () => {
  const languageStore = useLanguageStore()
  return [
    {
      id: 'home',
      title: translate('headerHome', languageStore.isEnglish),
      path: '/',
    },
    {
      id: 'article',
      title: translate('headerArticle', languageStore.isEnglish),
      children: [
        {
          id: 'note',
          title: translate('headerNote', languageStore.isEnglish),
          path: '/article/note',
        },
        {
          id: 'project',
          title: translate('headerProject', languageStore.isEnglish),
          path: '/article/project',
        },
        // {
        //   id: 'bug',
        //   title: translate('headerBug', languageStore.isEnglish),
        //   path: '/article/bug',
        // },
      ],
    },
    {
      id: 'tool',
      title: translate('headerTool', languageStore.isEnglish),
      path: '/tool',
    },
    // {
    //   id: 'media',
    //   title: translate('headerMedia', languageStore.isEnglish),
    //   children: [
    //     {
    //       id: 'mediaBook',
    //       title: translate('headerItemBook', languageStore.isEnglish),
    //       path: '/media/book',
    //     },
    //     {
    //       id: 'mediaPhoto',
    //       title: translate('headerItemPhoto', languageStore.isEnglish),
    //       path: '/media/photo',
    //     },
    //     {
    //       id: 'mediaVideo',
    //       title: translate('headerItemVideo', languageStore.isEnglish),
    //       path: '/media/video',
    //     },
    //     {
    //       id: 'mediaMusic',
    //       title: translate('headerItemMusic', languageStore.isEnglish),
    //       path: '/media/music',
    //     },
    //   ],
    // },
    // {
    //   id: 'board',
    //   title: translate('hederBoard', languageStore.isEnglish),
    //   path: '/board',
    // },
  ]
}
