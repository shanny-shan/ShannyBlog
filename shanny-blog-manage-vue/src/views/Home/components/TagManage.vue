<script setup>
import TagDialog from '@/views/_components/dialog/TagDialog.vue'
import { onMounted, ref } from 'vue'
import { useAdminStore, useTagStore, useSiteStore } from '@/stores'

const adminStore = useAdminStore()
const tagStore = useTagStore()
const siteStore = useSiteStore()

const tagList = ref([])

const getTagList = async () => {
  siteStore.loading = true
  const res = await tagStore.getTagList()
  console.log(res.data.data)
  if (res.data.code.toLowerCase() === 'success') {
    tagList.value = res.data.data || []
    siteStore.loading = false
  }
}

const editTag = (item) => {
  adminStore.openDialog('tag')
  tagStore.tagForm = item
  adminStore.isEdit = true
}
const deleteTag = (item) => {}

onMounted(() => {
  getTagList()
})
</script>
<template>
  <div>
    <div class="flex justify-end gap-2">
      <button class="btn btn-primary" @click="adminStore.openDialog('tag')">
        Add tag
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
            <th>Name</th>
            <th>NameEn</th>
            <th>Edit</th>
          </tr>
        </thead>
        <tbody>
          <tr v-for="item in tagList">
            <th>
              <label>
                <input type="checkbox" class="checkbox" />
              </label>
            </th>
            <td>{{ item.name }}</td>
            <td>{{ item.nameEn }}</td>
            <th>
              <div class="flex gap-2">
                <button class="btn btn-ghost btn-xs" @click="editTag(item)">
                  Edit
                </button>
                <button class="btn btn-ghost btn-xs" @click="deleteTag(item)">
                  Delete
                </button>
              </div>
            </th>
          </tr>
        </tbody>
      </table>
    </div>
  </div>
  <TagDialog />
</template>
<style lang="scss" scoped></style>
