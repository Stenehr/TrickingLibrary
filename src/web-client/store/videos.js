const initState = () => ({
    uploadPromise: null
})

export const state = initState;

export const mutations = {
    setUploadPromise(state, { uploadPromise }) {
        state.uploadPromise = uploadPromise;
    },

    reset(state) {
        Object.assign(state, initState())
    }
}

export const actions = {
    startVideoUpload({ commit }, { form }) {
        const uploadPromise = this.$axios.$post("http://localhost:5000/api/videos", form);
        commit("setUploadPromise", { uploadPromise })
    }
}
