<script setup>
import ProjectDialog from '@/views/_components/dialog/ProjectDialog.vue'
import { onMounted, ref } from 'vue'
import { useToast } from 'vue-toastification'
import { useAdminStore, useArticleStore } from '@/stores'
import { swal } from '@/utils/sweetalert'

const toast = useToast()
const adminStore = useAdminStore()
const articleStore = useArticleStore()

const editProject = (item) => {
  adminStore.openDialog('project')
  articleStore.articleForm = { ...item }
  adminStore.isEdit = true
}
const deleteProject = (item) => {
  swal(
    '',
    '',
    `确定删除标题为<span class="text-primary font-bold">${item.title}</span>的项目吗？`,
    'question',
    true,
    true,
  ).then(async (result) => {
    if (result.isConfirmed) {
      const res = await articleStore.deleteArticle(item.id)
      if (res.data.code.toLowerCase() === 'success') {
        toast.success(`${res.data.msg}`)
        await articleStore.getProjectList()
      } else {
        toast.error(`${res.data.msg}`)
      }
    }
  })
}

onMounted(async () => {
  await articleStore.getProjectList()
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
          <tr v-for="item in articleStore.projectList">
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
                <button class="btn btn-ghost btn-xs" @click="editProject(item)">
                  Edit
                </button>
                <button
                  class="btn btn-ghost btn-xs"
                  @click="deleteProject(item)"
                >
                  Delete
                </button>
              </div>
            </th>
          </tr>
        </tbody>
      </table>
    </div>
  </div>
  <ProjectDialog />
</template>
<style lang="scss" scoped></style>
