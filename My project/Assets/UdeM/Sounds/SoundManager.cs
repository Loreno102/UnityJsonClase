using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UdeM.Base;

namespace UdeM.Sounds
{
    public class SoundManager : CustomMonoBehaviour
    {
        [SerializeField] protected AudioSource _audioSource;
        [SerializeField] protected Dictionary<string, AudioClip> _audioClips;

        protected override void Awake()
        {
            base.Awake();
            _audioSource = GetComponent<AudioSource>();
            _audioClips = new Dictionary<string, AudioClip>();
            if ( _audioSource == null ) {
                _audioSource = gameObject.AddComponent<AudioSource>();
            }
        }

        public Dictionary<string, AudioClip> AudioClips {  get { return _audioClips; } }

        public void PrepareClip(string clipKey)
        {
            if (!_audioClips.ContainsKey(clipKey)) {
                Debug.LogWarning($"The AudioClip {clipKey} don't exists");
                return;
            }
            _audioSource.clip = _audioClips[clipKey];
            _audioSource.loop = true;
        }

        /**
        * Speed factor (1.0 is normal speed, 2.0 is double speed, 0.5 is half speed)
        * [Range(0.1f, 3.0f)] // This adds a slider in the inspector
        * */
        public void PlaySound(float playBackSpeed = 1.0f)
        {
            
            if (_audioSource.clip == null) {
                Debug.LogWarning("There is not sound setup to play");
                return;
            }
            _audioSource.pitch = playBackSpeed;
            if (_audioSource.isPlaying) { return; }
            _audioSource.Play();
        }

        public void StopSound() { 
            _audioSource.Stop();
        }

        public void PlayOneShotSound(string clipKey, int replay = 1)
        {
            if (!_audioClips.ContainsKey(clipKey)) return;
            StartCoroutine(PlaySoundCoroutine(clipKey, replay));
        }

        private IEnumerator PlaySoundCoroutine(string clipKey, int replay = 1)
        {
            _audioSource.PlayOneShot(_audioClips[clipKey]);
            yield return new WaitUntil(() => { return !_audioSource.isPlaying; });
            if( replay > 1 ) {
                PlayOneShotSound(clipKey, replay-1);
            }
        }

        public void InitClips(string path, List<string> clipsUrl)
        {
            foreach(var cl in clipsUrl) {
                InitClip(path, cl);
            }
        }

        public void InitClip(string path, string clipUrl)
        {
            AudioClip ac = Resources.Load<AudioClip>(path + clipUrl);
            if(ac == null) {
                Debug.LogWarning("The resource clip not exists: " + path + clipUrl);
            }
            _audioClips.Add(clipUrl, ac);
        }
    }
}