<script setup>
import ProjectDialog from '@/views/_components/dialog/ProjectDialog.vue'
import { onMounted, ref, computed } from 'vue'
import { useAdminStore, useArticleStore, usePageStore } from '@/stores'
import PaginationComponent from '@/views/_components/common/PaginationComponent.vue'

const adminStore = useAdminStore()
const articleStore = useArticleStore()
const pageStore = usePageStore()

const { pageList, totalPages } = pageStore.getPageData(
  () => articleStore.projectList,
)

onMounted(async () => {
  await articleStore.getArticleList('ARTICLE_PROJECT')
})
</script>
<template>
  <div>
    <div class="flex justify-end">
      <button class="btn btn-primary" @click="adminStore.openDialog('project')">
        Add Project
      </button>
    </div>
    <div class="overflow-x-auto mt-2">
      <table class="table">
        <thead>
          <tr>
            <th>
              <label>
                <input type="checkbox" class="checkbox" />
              </label>
            </th>
            <th>Title</th>
            <th>Memo</th>
            <th>Content</th>
            <th>Tags</th>
            <th>Published</th>
            <!-- <th>CreateTime</th> -->
            <th>UpdateTime</th>
            <th>Edit</th>
          </tr>
        </thead>
        <tbody>
          <tr v-for="item in pageList" :key="item.id">
            <th>
              <label>
                <input type="checkbox" class="checkbox" />
              </label>
            </th>
            <td>
              {{
                item.title.length > 10
                  ? item.title.slice(0, 10) + '...'
                  : item.title
              }}
            </td>
            <td>
              {{
                item.memo.length > 10
                  ? item.memo.slice(0, 10) + '...'
                  : item.memo
              }}
            </td>
            <td>
              {{
                item.content.length > 10
                  ? item.content.slice(0, 10) + '...'
                  : item.content
              }}
            </td>
            <td>
              <div class="flex flex-wrap gap-2">
                <div
                  v-for="tag in item.tagList"
                  :key="tag.id"
                  class="badge badge-soft badge-primary"
                >
                  {{ tag.name }}
                </div>
              </div>
            </td>
            <td>{{ item.published }}</td>
            <!-- <td>{{ item.createTime }}</td> -->
            <td>{{ item.updateTime?.substring(0, 10) }}</td>
            <th>
              <div class="flex gap-2">
                <button
                  class="btn btn-ghost btn-xs"
                  @click="articleStore.openEditArticle(item, 'project')"
                >
                  Edit
                </button>
                <button
                  class="btn btn-ghost btn-xs"
                  @click="articleStore.deleteArticle(item, 'ARTICLE_PROJECT')"
                >
                  Delete
                </button>
              </div>
            </th>
          </tr>
        </tbody>
      </table>
    </div>
    <div class="mt-2 md:mt-10 flex justify-center" v-if="totalPages > 1">
      <PaginationComponent
        :current-page="pageStore.currentPage"
        :total-pages="totalPages"
        :page-range="5"
        @page-change="pageStore.handlePageChange"
      />
    </div>
  </div>
  <ProjectDialog />
</template>
<style lang="scss" scoped></style>
