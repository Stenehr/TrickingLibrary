import { UPLOAD_TYPE } from "../data/enums";

const initState = () => ({
    uploadPromise: null,
    active: false,
    type: "",
})

export const state = initState;

export const mutations = {
    activate(state, { type }) {
        state.active = true;
        state.type = type;
    },

    setUploadPromise(state, { uploadPromise }) {
        state.uploadPromise = uploadPromise;
    },

    reset(state) {
        Object.assign(state, initState())
    }
}

export const actions = {
    startVideoUpload({ commit }, { form }) {
        const uploadPromise = this.$axios.$post("/api/videos", form);
        commit("setUploadPromise", { uploadPromise })
    }
}
